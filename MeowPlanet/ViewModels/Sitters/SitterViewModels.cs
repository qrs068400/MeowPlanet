﻿using MeowPlanet.Models;

namespace MeowPlanet.ViewModels.Sitters
{
    public class SitterViewModels
    {
        public Sitter? sitter { get; set; }

        public string? memberPhoto { get; set; }

        public List<string>? sitterfeatureList { get; set; }

        public List<Orderlist>? OrderCommentList { get; set; }

        public List<Cat>? usercatList { get; set; }

        public List<SitterWorkViewModel> sitterWorkViewModels { get; set; }
    }
}
