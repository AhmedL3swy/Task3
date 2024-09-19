namespace DataGrid.Application.Shared.Models
{
    public class SearchResult<DbSet> where DbSet : class
    {
        public List<DbSet> Data { get; set; }
        public int Total { get; set; }

    }
}
