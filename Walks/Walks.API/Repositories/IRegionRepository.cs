using Walks.API.Models;
using Walks.API.Models.DTO;

namespace Walks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> ListAsync();
        Task<Region?> CreateAsync(Region region);
        Task<Region?> ReadAsync(Guid id);
        Task<Region?> UpdateAsync(Guid id, UpdateRegionDTO region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
