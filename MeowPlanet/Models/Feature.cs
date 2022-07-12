using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class Feature
    {
        public Feature()
        {
            Services = new HashSet<Sitter>();
        }

        public int FeatureId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Sitter> Services { get; set; }
    }
}
