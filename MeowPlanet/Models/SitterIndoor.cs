using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class SitterIndoor
    {
        public SitterIndoor()
        {
            Sitters = new HashSet<Sitter>();
        }

        public int IndoorId { get; set; }
        public int Name { get; set; }

        public virtual ICollection<Sitter> Sitters { get; set; }
    }
}
