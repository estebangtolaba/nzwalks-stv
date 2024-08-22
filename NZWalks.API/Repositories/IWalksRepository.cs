using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterValue = null, string? sortBy = null, bool ascending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk> GetByIdAsync(Guid id);

        Task<Walk> CreateAsync(Walk walk);

        Task<Walk> DeleteAsync(Guid id);

        Task<Walk> UpdateAsync(Guid id, Walk walk);
    }
}
