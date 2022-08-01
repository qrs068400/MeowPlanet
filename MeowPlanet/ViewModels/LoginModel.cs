using MeowPlanet.Models;
using System.ComponentModel.DataAnnotations;

namespace MeowPlanet.ViewModels
{
    public class LoginModel : Member
    {
        [Required(ErrorMessage = "請輸入有效的Email")]
        [EmailAddress(ErrorMessage = "請輸入有效的Email")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "請輸入有效的密碼")]
        public string Password { get; set; } = null!;
    }
}
