using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class SitterHouse
    {
        public SitterHouse()
        {
            Sitters = new HashSet<Sitter>();
        }

        public int HouseId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Sitter> Sitters { get; set; }
    }
}
