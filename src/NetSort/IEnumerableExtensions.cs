using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSort
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key) where T : class
        {
            SortOperationMetadata metadata = SortOperationMetadataFinder.Find<T>(key);
			if (metadata == null)
			{
				string error = $"A 'sortable attribute' could not be found.\n Key: {key} \nType: {typeof(T).Name}.";
				throw new ArgumentException(error);
			}

            return Sort(items, metadata);
        }

        public static IEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, SortDirection direction)
            where T : class
        {
            SortOperationMetadata metadata = SortOperationMetadataFinder.Find<T>(key, direction);
			if (metadata == null)
			{
				string error = $"A 'sortable attribute' could not be found.\n Key: {key} \nType: {typeof(T).Name}.";
				throw new ArgumentException(error);
			}

            return Sort(items, metadata);
        }

        private static IEnumerable<T> Sort<T>(IEnumerable<T> items, SortOperationMetadata metadata)
        {
            if (metadata.Direction == SortDirection.Asc)
            {
                return items.OrderBy(i => metadata.ToSortBy.GetValue(i));
            }

            return items.OrderByDescending(i => metadata.ToSortBy.GetValue(i));
        }
    }
}
