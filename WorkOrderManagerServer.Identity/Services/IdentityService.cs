using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WorkOrderManagerServer.Application.Services;
using WorkOrderManagerServer.Application.DTOs.Response;
using WorkOrderManagerServer.Application.DTOs.Request;
using WorkOrderManagerServer.Identity.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using WorkOrderManagerServer.Application.DTOs.Models;

namespace WorkOrderManagerServer.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions.Value;
        }

        async Task<List<User>> IIdentityService.GetAllUsers()
        {
            var users = _userManager.Users;

            var response = new List<User>();
            
            foreach (var user in users)
            {
                var roles = string.Join(",", await _userManager.GetRolesAsync(user));
                response.Add(new User(user.Id, user.UserName, roles));
            }

            return response;
        }

        async Task<UserRegisterResponse> IIdentityService.Register(UserRegisterRequest user)
        {
            if (!await _roleManager.RoleExistsAsync("Basic"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Basic"));
                await _roleManager.CreateAsync(new IdentityRole("Collaborator"));
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var identityUser = new IdentityUser { UserName = user.UserName };

            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, "Basic");
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
            }

            var response = new UserRegisterResponse(result.Succeeded);
            if(!result.Succeeded && result.Errors.Any())
            {
                response.AddErrors(result.Errors.Select(r => r.Description));
            }

            return response;
        }

        async Task<UserLoginResponse> IIdentityService.Login(UserLoginRequest user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, true);

            UserLoginResponse response;

            if (result.Succeeded)
            {
                response = await GenerateToken(user.UserName);
            }
            else
            {
                response = new UserLoginResponse(result.Succeeded);

                if (result.IsLockedOut)
                {
                    response.AddError("Essa conta está bloqueada");
                }
                else if (result.IsNotAllowed)
                {
                    response.AddError("Essa conta não possui permissão para realizar login");
                }
                else
                {
                    response.AddError("Usuário ou senha estão incorretos");
                }
            }

            return response;
        }
        async Task<UserUpdateRoleResponse> IIdentityService.UpdateRole(UserUpdateRoleRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            UserUpdateRoleResponse response;
            if(user == null)
            {
                response = new UserUpdateRoleResponse(false);
                response.Errors.Add("Nome de usuário não encontrado");
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles != null && roles.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, roles);
                }
                await _userManager.AddToRoleAsync(user, request.Role);

                response = new UserUpdateRoleResponse(true);
            }

            return response;
        }

        private async Task<UserLoginResponse> GenerateToken(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = await GetClaims(user);

            var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UserLoginResponse(success: true, token: token, expirationDate: expirationDate);
        }

        private async Task<IList<Claim>> GetClaims(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }
    }
}
