using Microsoft.AspNetCore.Identity;
using WorkOrderManagerServer.Application.DTOs.Models;
using WorkOrderManagerServer.Application.DTOs.Request;
using WorkOrderManagerServer.Application.DTOs.Response;

namespace WorkOrderManagerServer.Application.Services
{
    public interface IIdentityService
    {
        Task<List<User>> GetAllUsers();
        Task<UserRegisterResponse> Register(UserRegisterRequest user);
        Task<UserLoginResponse> Login(UserLoginRequest user);
        Task<UserUpdateRoleResponse> UpdateRole(UserUpdateRoleRequest request);
    }
}