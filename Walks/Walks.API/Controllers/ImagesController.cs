using Microsoft.AspNetCore.Mvc;
using Walks.API.Models;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository repository;

        public ImagesController(IImageRepository imageRepository)
        {
            repository = imageRepository;
        }


        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO imageUploadDTO)
        {
            ValidateFileUpload(imageUploadDTO);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = new Image
            {
                File = imageUploadDTO.File,
                Extension = Path.GetExtension(imageUploadDTO.File.FileName),
                Size = imageUploadDTO.File.Length,
                Name = imageUploadDTO.Name,
                Description = imageUploadDTO.Description
            };
            
            await repository.Upload(image);

            return Ok(image);
        }

        private void ValidateFileUpload(ImageUploadDTO imageUploadDTO)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if(!allowedExtensions.Contains(Path.GetExtension(imageUploadDTO.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(imageUploadDTO.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Fize size cannot be more than 10MB");
            }
        }
    }
}
