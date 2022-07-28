using MeowPlanet.Models;
namespace MeowPlanet.ViewModels
{
    public class SitterViewModels
    {
        public Sitter? sitter { get; set; }

        public string? memberPhoto { get; set; }

        public List<string>? sitterfeatureList { get; set; }

        public List<Orderlist>? OrderCommentList { get; set; }
    }
}
