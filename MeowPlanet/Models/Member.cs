using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class Member
    {
        public Member()
        {
            Adopts = new HashSet<Adopt>();
            Cats = new HashSet<Cat>();
            Clues = new HashSet<Clue>();
            Orderlists = new HashSet<Orderlist>();
            Sitters = new HashSet<Sitter>();
            Services = new HashSet<Sitter>();
        }

        public int MemberId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Adopt> Adopts { get; set; }
        public virtual ICollection<Cat> Cats { get; set; }
        public virtual ICollection<Clue> Clues { get; set; }
        public virtual ICollection<Orderlist> Orderlists { get; set; }
        public virtual ICollection<Sitter> Sitters { get; set; }

        public virtual ICollection<Sitter> Services { get; set; }
    }
}
