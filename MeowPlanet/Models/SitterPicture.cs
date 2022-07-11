using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class SitterPicture
    {
        public int PicId { get; set; }
        public int ServiceId { get; set; }
        public int PicOrder { get; set; }
        public string Path { get; set; } = null!;

        public virtual Sitter Service { get; set; } = null!;
    }
}
