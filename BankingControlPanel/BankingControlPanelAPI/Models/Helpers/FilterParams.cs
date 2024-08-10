using System.Text.Json.Serialization;

namespace BankingControlPanelAPI.Models.Helpers
{
    public class FilterParams
    {
        [JsonIgnore]
        public string UserId { get; set; }
        public string SearchParam {  get; set; }
        public string SortBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
