using System.Diagnostics.Eventing.Reader;

namespace Shopping.Web.Models.Ordering
{
    public class PaginatedResult<TEntity>(int pageIndex, int pageSize, long count, 
        IEnumerable<TEntity> data) where TEntity : class
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public long Count { get; set; } = count;
        public IEnumerable<TEntity> Data { get; } = data;
    }
}
