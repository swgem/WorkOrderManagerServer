using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WorkOrderManagerServer.Application.DTOs.Request;
using WorkOrderManagerServer.Application.DTOs.Response;
using WorkOrderManagerServer.Application.Models;
using WorkOrderManagerServer.Application.Services;

namespace WorkOrderManagerServer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService) =>
            _identityService = identityService;

        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _identityService.Register(user);
            if (result.Success)
            {
                return Ok(result);
            }
            else if (result.Errors.Any())
            {
                return BadRequest(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _identityService.Login(user);
            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Unauthorized(result);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("update_role")]
        public async Task<ActionResult<List<User>>> UpdateRole(UserUpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var data = await _identityService.UpdateRole(request);
            return Ok(data);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var data = await _identityService.GetAllUsers();
            return Ok(data);
        }
    }
}
