using Walks.API.Models;

namespace Walks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> ListAsync();
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk> ReadAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);
    }
}
