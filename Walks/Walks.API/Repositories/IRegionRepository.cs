﻿using Walks.API.Models;

namespace Walks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> ListAsync();
        Task<Region> CreateAsync(Region region);
        Task<Region> ReadAsync(Guid id);
        Task<Region> UpdateAsync(Guid id, Region region);
        Task<Region> DeleteAsync(Guid id);
    }
}
