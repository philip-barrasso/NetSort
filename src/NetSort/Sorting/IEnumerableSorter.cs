using System.Collections.Generic;
using System.Linq;

namespace NetSort.Sorting
{
    internal class IEnumerableSorter<T> : BaseSorter<IEnumerable<T>, IOrderedEnumerable<T>, T> where T : class
    {
        protected override IOrderedEnumerable<T> DoSortAsc(IEnumerable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.OrderBy(i => GetNestedValue(metadata, i));
        }

        protected override IOrderedEnumerable<T> DoSortDesc(IEnumerable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            return items.OrderByDescending(i => GetNestedValue(metadata, i));
        }
    }
}
