namespace MeowPlanet.ViewModels.Missings
{
    public class ItemsViewModel
    {
        public int MissingId { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Breed { get; set; }
        public int ClueCount { get; set; }
        public DateTime MissingDate { get; set; }        
        public DateTime? UpdateDate { get; set; }

    }
}
