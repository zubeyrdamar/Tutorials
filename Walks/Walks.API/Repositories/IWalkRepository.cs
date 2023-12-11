using Walks.API.Models;

namespace Walks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> ListAsync(string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 20);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk> ReadAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);
    }
}
