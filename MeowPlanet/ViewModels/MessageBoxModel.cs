using MeowPlanet.Models;


namespace MeowPlanet.ViewModels
{
    public class MessageBoxModel
    {
        public string userName { get; set; }
        public string userPhoto { get; set; }
        public ICollection<ContactMembers> ContactMembers { get; set; }

    }
}
