using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models;

namespace Walks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WalksDbContext _context;
        public SQLRegionRepository(WalksDbContext context)
        {
            _context = context;
        }

        public async Task<List<Region>> ListAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> ReadAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var tempRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (tempRegion == null)
            {
                return null;
            }

            tempRegion.Code = region.Code;
            tempRegion.Name = region.Name;
            tempRegion.ImageUrl = region.ImageUrl;
            await _context.SaveChangesAsync();

            return tempRegion;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = _context.Regions.FirstOrDefault(r => r.Id == id);
            if(region == null) { return null; }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return region;
        }
    }
}
