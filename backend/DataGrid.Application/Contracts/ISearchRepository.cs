using DataGrid.Application.Shared.Models;

namespace DataGrid.Application.Contracts
{
    public interface ISearchRepository<DbSet> where DbSet : class
    {
        Task<SearchResult<DbSet>> SearchAsync(SearchQuery query);
    }
}
