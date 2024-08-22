using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SqlRegionRepository(NZWalksDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public Task<Region> CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public Task<Region> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> UpdateAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Region>> IRegionRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
