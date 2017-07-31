using NetSort.Sorting;
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
        public static IOrderedEnumerable<T> ThenSortByKey<T>(this IOrderedEnumerable<T> items, string key, char delimiter) where T : class
        {
            return new IEnumerableThenBySorter<T>()
                .Sort(items, key, delimiter, null);
        }

        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' and 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        /// <param name="delimiter">The char used to separate nested properties</param>
        public static IOrderedEnumerable<T> ThenSortByKey<T>(this IOrderedEnumerable<T> items, string key, char delimiter, SortDirection direction) where T : class
        {
            return new IEnumerableThenBySorter<T>()
                .Sort(items, key, delimiter, direction);
        }

        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' in the order specified on the 'SortableAttribute' of the key passed in, or the default (Ascending).
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        public static IOrderedEnumerable<T> ThenSortByKey<T>(this IOrderedEnumerable<T> items, string key) where T : class
        {
            return new IEnumerableThenBySorter<T>()
                .Sort(items, key, Constants.DEFAULT_NESTED_PROP_DELIMITER, null);
        }        

        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' and 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        public static IOrderedEnumerable<T> ThenSortByKey<T>(this IOrderedEnumerable<T> items, string key, SortDirection direction) where T : class
        {
            return new IEnumerableThenBySorter<T>()
                .Sort(items, key, Constants.DEFAULT_NESTED_PROP_DELIMITER, direction);
        }
        
        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' in the specified 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        /// <param name="delimiter">The char used to separate nested properties</param>
        public static IOrderedEnumerable<T> ThenSortByKey<T>(this IOrderedEnumerable<T> items, string key, char delimiter, string direction) where T : class
        {
            return new IEnumerableThenBySorter<T>()
                .SortWithStringDirection(items, key, delimiter, direction);
        }

        /// <summary>
        /// Returns an ordered enumerable per the specified 'key' in the specified 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        public static IOrderedEnumerable<T> ThenSortByKey<T>(this IOrderedEnumerable<T> items, string key, string direction) where T : class
        {
            return new IEnumerableThenBySorter<T>()
                .SortWithStringDirection(items, key, Constants.DEFAULT_NESTED_PROP_DELIMITER, direction);
        }
    }
}
