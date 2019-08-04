using Database.Repository.Features.ProfileManagement.Register;
using System.ComponentModel.DataAnnotations;

namespace NotThisTime.Controllers.Register
{
    public class RegisterBodyData
    {
        [Required]
        [MinLength(5)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        public string FullName { get; set; }
    }

    public static class RegisterBodyDataToDTO
    {
        public static RegisterDTO ToDTO(this RegisterBodyData data)
        {
            return new RegisterDTO
            {
                Username = data.Username,
                Email = data.Email,
                FullName = data.FullName,
                Password = data.Password,
                IsActive = true
            };
        }
    }
}
