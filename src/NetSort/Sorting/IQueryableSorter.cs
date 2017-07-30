using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSort.Sorting
{
    internal class IQueryableSorter<T> : BaseSorter<IQueryable<T>, IOrderedQueryable<T>, T> where T : class
    {
        protected override IOrderedQueryable<T> DoSortAsc(IQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.OrderBy(i => GetNestedValue(metadata, i));
        }

        protected override IOrderedQueryable<T> DoSortDesc(IQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.OrderByDescending(i => GetNestedValue(metadata, i));
        }
    }
}
