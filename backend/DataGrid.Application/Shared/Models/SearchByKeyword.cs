using Newtonsoft.Json;

namespace DataGrid.Application.Shared.Models
{
    /// <summary>
    /// Represents a search operation by keyword.
    /// </summary>
    public class SearchByKeyword
    {
        /// <summary>
        /// Gets or sets the comma-separated list of fields to search in will do "OR" search include both Result Set.
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// Gets or sets the keyword(Value) to search for.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets the list of fields as a collection of strings.
        /// </summary>
        [JsonIgnore]
        public List<string> FieldList
        {
            get
            {
                return Fields.Split(',').ToList();
            }
        }
    }
}
