using System.ComponentModel.DataAnnotations.Schema;

namespace Walks.API.Models
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public string Path { get; set; }
    }
}
