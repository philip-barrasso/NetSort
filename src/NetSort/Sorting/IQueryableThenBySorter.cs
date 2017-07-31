using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSort.Sorting
{
    internal class IQueryableThenBySorter<T> : BaseQueryableSorter<IOrderedQueryable<T>, IOrderedQueryable<T>, T> where T : class
    {
        protected override IOrderedQueryable<T> DoSortAsc(IOrderedQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return SortByExpression(items, metadata, "ThenBy");
        }

        protected override IOrderedQueryable<T> DoSortDesc(IOrderedQueryable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return SortByExpression(items, metadata, "ThenByDescending");
        }
    }
}
