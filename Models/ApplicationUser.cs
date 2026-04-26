using Microsoft.AspNetCore.Identity;

namespace AttendanceTrackerMicroservices.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
