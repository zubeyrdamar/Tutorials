using Walks.API.Data;
using Walks.API.Models;

namespace Walks.API.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly WalksDbContext context;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, WalksDbContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        public async Task<Image> Upload(Image image)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", image.Name + image.Extension);

            using var stream = new FileStream(localPath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlPath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.Name}{image.Extension}";
            image.Path = urlPath;

            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();

            return image;
        }
    }
}
