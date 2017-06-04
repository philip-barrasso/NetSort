using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSort
{
    public static class SortableExtensions
    {
        /// <summary>
        /// Returns a sorted enumerable per the 'key' passed in. The order will be the order specified on the 'SortableAttribute' of the key passed in, or the default (Ascending).
        /// </summary>
        /// <typeparam name="T">The type of data making up the data to sort</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
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

        /// <summary>
        /// Returns a sorted enumerable per the 'key' passed in and in the order passed in.
        /// </summary>
        /// <typeparam name="T">The type of data making up the data to sort</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Ascending | Descending)</param>
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
