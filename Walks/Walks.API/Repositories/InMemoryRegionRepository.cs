using Walks.API.Models;

namespace Walks.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public Task<List<Region>> List()
        {
            return Task.FromResult(new List<Region>
            {
                new() {
                    Id = Guid.NewGuid(),
                    Name = "İzmir",
                    Code = "35"
                }
            });
        }
    }
}
