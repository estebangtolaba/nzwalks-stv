using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalksRepository
    {
        private readonly NZWalksDbContext dbContext;

        public WalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await dbContext.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (walk == null)
            {
                return null;
            }

            // Delete the region
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterValue = null, string? sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterValue) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterValue));
                }
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = ascending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                } else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = ascending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination

            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Description = walk.Description;
            existingWalk.Name = walk.Name;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.Difficulty = walk.Difficulty;
            existingWalk.Region = walk.Region;


            await dbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
