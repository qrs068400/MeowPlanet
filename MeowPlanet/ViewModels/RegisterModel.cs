using MeowPlanet.Models;
using System.ComponentModel.DataAnnotations;

namespace MeowPlanet.ViewModels
{
    public class RegisterModel : Member
    {
        public int MemberId { get; set; }
        [Required(ErrorMessage = "請輸入")]
        [EmailAddress(ErrorMessage = "無效")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "請輸入")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "請輸入")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "請輸入")]
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
    }
}
