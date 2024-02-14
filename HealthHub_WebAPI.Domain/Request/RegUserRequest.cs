using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.Request
{
   
    public class RegUserRequest
    {
        [Required(ErrorMessage = "User name is required!")]
        [RegularExpression(@"^[a-zA-Z0-9]{5,20}$", ErrorMessage = "Username must be between 5 and 20 characters and can only contain letters and numbers.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
    }

}
