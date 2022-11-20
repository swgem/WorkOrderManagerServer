using WorkOrderManagerServer.Application.DTOs.Request;
using WorkOrderManagerServer.Application.DTOs.Response;

namespace WorkOrderManagerServer.Application.Services
{
    public interface IIdentityService
    {
        Task<UserRegisterResponse> Register(UserRegisterRequest user);
        Task<UserLoginResponse> Login(UserLoginRequest user);
    }
}