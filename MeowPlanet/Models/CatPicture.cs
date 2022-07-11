using System;
using System.Collections.Generic;

namespace MeowPlanet.Models
{
    public partial class CatPicture
    {
        public int PicId { get; set; }
        public int CatId { get; set; }
        public int PicOrder { get; set; }
        public string Path { get; set; } = null!;

        public virtual Cat Cat { get; set; } = null!;
    }
}
