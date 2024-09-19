using DataGrid.Application.Contracts;
using DataGrid.Application.Shared.Models;
using DataGrid.Persistence.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DataGrid.Persistence.Repositories
{
    /// <summary>
    /// Represents a repository for searching entities in version 2.
    /// </summary>
    /// <typeparam name="DbSet">The type of the entity set.</typeparam>
    public class SearchRepository<DbSet> : ISearchRepository<DbSet> where DbSet : class
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchRepository{DbSet}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SearchRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Searches for entities based on the provided query.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>The search result.</returns>
        public async Task<SearchResult<DbSet>> SearchAsync(SearchQuery query)
        {
            IQueryable<DbSet> entities = _context.Set<DbSet>().AsNoTracking();

            // Apply Keyword Search
            entities = entities.ApplyKeywordSearch(query.SearchByKeyword);

            // Apply Range Search
            entities = entities.ApplyRangeSearch(query.RangeSearch);

            // Apply Sorting
            entities = entities.ApplySorting(query.SortBy, query.SortDirection);

            // Apply Includes for related fields
            entities = entities.ApplyIncludes(query.Include);

            // Pagination
            var total = await entities.CountAsync();
            entities = entities.ApplyPaging(query.PageNumber, query.PageSize);

            var result = await entities.ToListAsync();

            return new SearchResult<DbSet>
            {
                Data = result,
                Total = total
            };
        }
    }
}
