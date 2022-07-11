using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class SitterOutdoor
    {
        public SitterOutdoor()
        {
            Sitters = new HashSet<Sitter>();
        }

        public int OutdoorId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Sitter> Sitters { get; set; }
    }
}
