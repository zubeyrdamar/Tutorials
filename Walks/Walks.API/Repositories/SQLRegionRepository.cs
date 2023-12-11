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

        public async Task<List<Region>> List()
        {
            return await _context.Regions.ToListAsync();
        }
    }
}
