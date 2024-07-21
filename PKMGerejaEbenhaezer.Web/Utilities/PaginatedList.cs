namespace PKMGerejaEbenhaezer.Web.Utilities
{
    public class PaginatedList<T> : List<T> 
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            if (pageIndex <= 0) 
                throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, $"0 or negative");

            TotalPages = (int)Math.Max(Math.Ceiling(count / (double)pageSize), 1);

            if (pageIndex > TotalPages) 
                throw new ArgumentOutOfRangeException(nameof(pageIndex), pageIndex, $"is greater than Total Pages : {TotalPages}");

            PageIndex = pageIndex;

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Empty()
        {
            return new PaginatedList<T>(new List<T>(), 0, 1, 1);
        }

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            var totalPages = (int)Math.Max(Math.Ceiling(count / (double)pageSize), 1);

            if (pageIndex > totalPages) pageIndex = totalPages;

            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
