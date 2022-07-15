using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeowPlanet.Models
{
    public partial class Member
    {
        public Member()
        {
            Adopts = new HashSet<Adopt>();
            Cats = new HashSet<Cat>();
            Clues = new HashSet<Clue>();
            Favorites = new HashSet<Favorite>();
            Orderlists = new HashSet<Orderlist>();
            Sitters = new HashSet<Sitter>();
        }

        public int MemberId { get; set; }

        [Required(ErrorMessage = "必填")]
        //[Remote(action: "VerifyEmail", controller: "Login")]
        [EmailAddress(ErrorMessage = "格式不符")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "必填")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "必填")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "必填")]
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }

        public virtual ICollection<Adopt> Adopts { get; set; }
        public virtual ICollection<Cat> Cats { get; set; }
        public virtual ICollection<Clue> Clues { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Orderlist> Orderlists { get; set; }
        public virtual ICollection<Sitter> Sitters { get; set; }
    }
}
