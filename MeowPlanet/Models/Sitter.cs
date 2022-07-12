using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class Sitter
    {
        public Sitter()
        {
            Orderlists = new HashSet<Orderlist>();
            Features = new HashSet<Feature>();
            Members = new HashSet<Member>();
        }

        public int ServiceId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Sleep { get; set; } = null!;
        public string House { get; set; } = null!;
        public string Outdoor { get; set; } = null!;
        public int Pay { get; set; }
        public decimal PosLat { get; set; }
        public decimal PosLng { get; set; }
        public bool IsService { get; set; }
        public string? Img01 { get; set; }
        public string? Img02 { get; set; }
        public string? Img03 { get; set; }
        public string? Img04 { get; set; }
        public string? Img05 { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual ICollection<Orderlist> Orderlists { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
