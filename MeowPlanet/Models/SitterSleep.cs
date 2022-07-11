using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class SitterSleep
    {
        public SitterSleep()
        {
            Sitters = new HashSet<Sitter>();
        }

        public int SleepId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Sitter> Sitters { get; set; }
    }
}
