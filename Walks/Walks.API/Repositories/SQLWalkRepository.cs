using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models;

namespace Walks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly WalksDbContext context;
        public SQLWalkRepository(WalksDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Walk>> ListAsync(string filterOn = null, string filterQuery = null)
        {
            var walks = context.Walks.Include(w => w.Difficulty).Include(w => w.Region).AsQueryable();
            if(string.IsNullOrEmpty(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false) 
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));
                }
            }
            return await walks.ToListAsync();
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> ReadAsync(Guid id)
        {
            var walk = await context.Walks.Include(w => w.Difficulty).Include(w => w.Region).FirstOrDefaultAsync(w => w.Id == id);
            if (walk == null) { return null; }
            return walk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var tempWalk = await context.Walks.FindAsync(id);
            if(tempWalk == null) { return null; }

            tempWalk.Name = walk.Name;
            tempWalk.Description = walk.Description;
            tempWalk.LengthInKm = walk.LengthInKm;
            tempWalk.ImageUrl = walk.ImageUrl;
            tempWalk.Difficulty = walk.Difficulty;
            tempWalk.Region = walk.Region;
        
            await context.SaveChangesAsync();

            return tempWalk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await context.Walks.FindAsync(id);
            if (walk == null) { return null; }
            context.Remove(walk);
            await context.SaveChangesAsync();
            return walk;
        }
    }
}
