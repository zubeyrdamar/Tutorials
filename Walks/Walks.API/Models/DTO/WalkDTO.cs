namespace Walks.API.Models.DTO
{
    public class WalkDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string ImageUrl { get; set; }

        public DifficultyDTO Difficulty { get; set; }
        public RegionDTO Region { get; set; }
    }
}
