using MeowPlanet.Models;
using System.ComponentModel.DataAnnotations;

namespace MeowPlanet.ViewModels
{
    public class RegisterModel : Member
    {
        public int MemberId { get; set; }
        [Required(ErrorMessage = "請輸入有效的Email")]
        [EmailAddress(ErrorMessage = "請輸入有效的Email")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "請輸入有效的密碼")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "請輸入有效的手機號碼")]
        
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "請輸入有效的名字")]
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
    }
}
