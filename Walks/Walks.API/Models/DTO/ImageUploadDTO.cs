namespace Walks.API.Models.DTO
{
    public class ImageUploadDTO
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
