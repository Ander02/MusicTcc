namespace Business.Models.Pagination
{
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> Items { get; set; } = [];

        public PagingDataResult PagingData { get; set; }

        public PagedResult()
        {
            PagingData = new PagingDataResult();
            Items = [];
        }

        public PagedResult(PagedRequest<T> request, int total, IEnumerable<T> items)
        {
            PagingData = new PagingDataResult(request, total);
            Items = items;
        }

        public class PagingDataResult
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
            public string Field { get; set; }
            public SortDirection? SortDirection { get; set; }

            public int TotalCount { get; set; }
            public int PageCount => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
            public bool IsFirstPage => Page == 1;
            public bool IsLastPage => PageCount == Page;

            public PagingDataResult() { }

            public PagingDataResult(PagedRequest<T> request, int total)
            {
                Page = request.Page;
                PageSize = request.PageSize;
                Field = request.Field;
                SortDirection = request.SortDirection;
                TotalCount = total;
            }
        }
    }
}
