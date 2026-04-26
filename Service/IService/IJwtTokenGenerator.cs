using AttendanceTrackerMicroservices.AuthAPI.Models;

namespace AttendanceTrackerMicroservices.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
