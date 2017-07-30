using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetSort.Sorting
{
    internal class IEnumerableThenBySorter<T> : BaseSorter<IOrderedEnumerable<T>, IOrderedEnumerable<T>, T> where T : class
    {
        protected override IOrderedEnumerable<T> DoSortAsc(IOrderedEnumerable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.ThenBy(i => GetNestedValue(metadata, i));
        }

        protected override IOrderedEnumerable<T> DoSortDesc(IOrderedEnumerable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.ThenByDescending(i => GetNestedValue(metadata, i));
        }
    }
}
