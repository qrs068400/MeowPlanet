using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class Sitter
    {
        public Sitter()
        {
            Orderlists = new HashSet<Orderlist>();
            SitterPictures = new HashSet<SitterPicture>();
            Members = new HashSet<Member>();
        }

        public int ServiceId { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public int SleepId { get; set; }
        public int HouseId { get; set; }
        public int IndoorId { get; set; }
        public int OutdoorId { get; set; }
        public int Pay { get; set; }
        public decimal PosLat { get; set; }
        public decimal PosLng { get; set; }
        public bool IsService { get; set; }

        public virtual SitterHouse House { get; set; } = null!;
        public virtual SitterIndoor Indoor { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
        public virtual SitterOutdoor Outdoor { get; set; } = null!;
        public virtual SitterSleep Sleep { get; set; } = null!;
        public virtual ICollection<Orderlist> Orderlists { get; set; }
        public virtual ICollection<SitterPicture> SitterPictures { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}
