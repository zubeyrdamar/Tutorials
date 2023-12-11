using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTO
{
    public class CreateRegionDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Code must contain 2 digits")]
        [MaxLength(2, ErrorMessage = "Code must contain 2 digits")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
