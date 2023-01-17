namespace dot.Models
{
    public class QueryParameters
    {
        const int maxPageSize = 5000;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 1000;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string OrderBy { get; set; } = "Date";
        public string? Type { get; set; }
        public string? SensorId { get; set; }
        public string? Since { get; set; }
        public string? Until { get; set; }
    }
}