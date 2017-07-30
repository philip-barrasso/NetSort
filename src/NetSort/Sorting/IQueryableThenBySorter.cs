using System.Collections.Generic;
using System.Linq;

namespace NetSort.Sorting
{
    internal class IQueryableThenBySorter<T> : BaseSorter<IOrderedQueryable<T>, IOrderedQueryable<T>, T> where T : class
    {
        protected override IOrderedQueryable<T> DoSortAsc(IOrderedQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.ThenBy(i => GetNestedValue(metadata, i));
        }

        protected override IOrderedQueryable<T> DoSortDesc(IOrderedQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.ThenByDescending(i => GetNestedValue(metadata, i));
        }
    }
}
