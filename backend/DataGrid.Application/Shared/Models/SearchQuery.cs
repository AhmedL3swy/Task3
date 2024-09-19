using System.Text.Json.Serialization;

namespace DataGrid.Application.Shared.Models
{
    /// <summary>
    /// Represents a search query with additional properties for version 2.
    /// </summary>
    public class SearchQuery
    {

        /// <summary>
        /// Gets or sets the list of search keywords that will be Used to search the data. * by Cascading Search "AND"
        /// </summary>
        [JsonIgnore]
        public List<SearchByKeyword> SearchByKeyword { get; set; } = new List<SearchByKeyword>();

        /// <summary>
        /// Gets or sets the list of range searches  by Cascading Search "AND".
        /// </summary>
        [JsonIgnore]
        public List<RangeSearch> RangeSearch { get; set; } = new List<RangeSearch>();
        /// <summary>
        /// The Name of the Property to sort by.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// The direction to sort the results 0 for Ascending and 1 for Descending.
        /// </summary>
        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

        /// <summary>
        /// The page number for pagination.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        ///  The number of items per page.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        ///  Include related entities Seperated By Comma Ex: "Category,User" .
        /// </summary>
        [JsonIgnore]
        public string Include { get; set; }
    }

}
