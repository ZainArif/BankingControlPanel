namespace BankingControlPanelAPI.Models.Helpers
{
    public class PaginatedResponse<TModel>
    {
        public IEnumerable<TModel> Values { get; set; }
        public int TotalValues { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
