using MeowPlanet.Models;
using System.ComponentModel.DataAnnotations;

namespace MeowPlanet.ViewModels
{
    public class LoginModel : Member
    {
        [Required(ErrorMessage = "請輸入")]
        [EmailAddress(ErrorMessage = "無效")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "請輸入")]
        public string Password { get; set; } = null!;
    }
}
