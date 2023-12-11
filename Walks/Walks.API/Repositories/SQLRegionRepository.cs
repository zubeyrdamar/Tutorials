using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models;
using Walks.API.Models.DTO;

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

        public async Task<Region?> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> ReadAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, UpdateRegionDTO regionDTO)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return null;
            }

            region.Code = regionDTO.Code;
            region.Name = regionDTO.Name;
            region.ImageUrl = regionDTO.ImageUrl;
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var region = _context.Regions.FirstOrDefault(r => r.Id == id);
            if(region == null) { return null; }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return region;
        }
    }
}
