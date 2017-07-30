using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSort
{
    public static partial class SortableExtensions
    {
        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' in the order specified on the 'SortableAttribute' of the key passed in, or the default (Ascending).
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="delimiter">The char used to separate nested properties</param>
        public static IOrderedEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, char delimiter) where T : class
        {
            return DoSort(items, key, delimiter, null);
        }

        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' and 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        /// <param name="delimiter">The char used to separate nested properties</param>
        public static IOrderedEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, char delimiter, SortDirection direction) where T : class
        {
            return DoSort(items, key, delimiter, direction);
        }

        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' in the order specified on the 'SortableAttribute' of the key passed in, or the default (Ascending).
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        public static IOrderedEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key) where T : class
        {
            return SortByKey(items, key, Constants.DEFAULT_NESTED_PROP_DELIMITER);
        }
        
        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' and 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        public static IOrderedEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, SortDirection direction) where T : class
        {
            return SortByKey(items, key, Constants.DEFAULT_NESTED_PROP_DELIMITER, direction);
        }
                
        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' in the specified 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        /// <param name="delimiter">The char used to separate nested properties</param>
        public static IOrderedEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, char delimiter, string direction) where T : class
        {
            var directionEnum = ParseDirection(direction);
            return SortByKey(items, key, delimiter, directionEnum);
        }

        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' in the specified 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        public static IOrderedEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, string direction) where T : class
        {
            return SortByKey(items, key, Constants.DEFAULT_NESTED_PROP_DELIMITER, direction);
        }

        private static IOrderedEnumerable<T> DoSort<T>(IEnumerable<T> items, string key, char delimiter, SortDirection? direction) where T : class
        {
            var metadata = SortOperationMetadataFinder.Find<T>(key, delimiter, direction);
            if (!metadata.Any())
            {
                var error = $"A 'sortable attribute' could not be found.\n Key: {key} \nType: {typeof(T).Name}.";
                throw new ArgumentOutOfRangeException(nameof(key), error);
            }

            return Sort(items, metadata);
        }

        private static SortDirection ParseDirection(string val)
        {
            SortDirection parsedDirection;
            var wasParseSuccessful = Enum.TryParse(val, true, out parsedDirection);
            if (wasParseSuccessful)
            {
                return parsedDirection;
            }

            throw new ArgumentOutOfRangeException("direction", $"{val} is not a valid 'SortDirection'");
        }

        private static IOrderedEnumerable<T> Sort<T>(IEnumerable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            if (metadata.Last().Direction == SortDirection.Asc)
            {
                return items.OrderBy(i => GetNestedValue(metadata, i));
            }
            else
            {
                return items.OrderByDescending(i => GetNestedValue(metadata, i));
            }
        }

        private static object GetNestedValue(IEnumerable<SortOperationMetadata> metadatas, object rootObj)
        {         
            foreach (var metadata in metadatas)
            {
                rootObj = metadata.ToSortBy.GetValue(rootObj);
            }

            return rootObj;
        }
    }
}
