using AttendanceTrackerMicroservices.AuthAPI.Models;
using AttendanceTrackerMicroservices.AuthAPI.Models.DTO;

namespace AttendanceTrackerMicroservices.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
        Task<ApplicationUser?> ValidateUser(LoginRequestDTO loginRequestDTO);
    }
}
