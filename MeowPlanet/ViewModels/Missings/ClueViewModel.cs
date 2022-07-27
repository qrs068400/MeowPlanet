namespace MeowPlanet.ViewModels.Missings
{
    public class ClueViewModel
    {
        public int ClueId { get; set; }
        public DateTime WitnessTime { get; set; }
        public int Status { get; set; }
        public string ImagePath { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Distance { get; set; }
        public string ProviderName { get; set; } = null!;
        public string ProviderPhoto { get; set; } = null!;

    }
}
