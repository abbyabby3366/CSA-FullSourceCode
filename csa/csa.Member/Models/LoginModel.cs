using System;
using System.ComponentModel.DataAnnotations;

namespace csa.Member.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }
    }
}