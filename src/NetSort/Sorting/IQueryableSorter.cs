using System.Collections.Generic;
using System.Linq;

namespace NetSort.Sorting
{
    internal class IQueryableSorter<T> : BaseQueryableSorter<IQueryable<T>, IOrderedQueryable<T>, T> where T : class
    {
        protected override IOrderedQueryable<T> DoSortAsc(IQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return SortByExpression(items, metadata, "OrderBy");
        }

        protected override IOrderedQueryable<T> DoSortDesc(IQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return SortByExpression(items, metadata, "OrderByDescending");
        }
    }
}
