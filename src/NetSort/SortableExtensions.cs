﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSort
{
    public static class SortableExtensions
    {
        /// <summary>
        /// Returns a sorted enumerable per the specified 'key' in the order specified on the 'SortableAttribute' of the key passed in, or the default (Ascending).
        /// </summary>
        /// <typeparam name="T">The type of data in the enumerable being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        public static IEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key) where T : class
        {
            IEnumerable<SortOperationMetadata> metadata = SortOperationMetadataFinder.Find<T>(key);
			if (metadata.Any() == false)
			{
				string error = $"A 'sortable attribute' could not be found.\n Key: {key} \nType: {typeof(T).Name}.";
				throw new ArgumentException(error);
			}

            return Sort(items, metadata);
        }

        /// <summary>
        /// Returns a sorted enumerable per the specified 'key' and 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data in the enumerable being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        public static IEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, SortDirection direction)
            where T : class
        {
            IEnumerable<SortOperationMetadata> metadata = SortOperationMetadataFinder.Find<T>(key, direction);
			if (metadata.Any() == false)
			{
				string error = $"A 'sortable attribute' could not be found.\n Key: {key} \nType: {typeof(T).Name}.";
				throw new ArgumentException(error);
			}

            return Sort(items, metadata);
        }

        /// <summary>
        /// Returns a sorted enumerable per the specified 'key' in the specified 'order'.
        /// </summary>
        /// <typeparam name="T">The type of data in the enumerable being sorted</typeparam>
        /// <param name="items">The data to sort</param>
        /// <param name="key">The 'key' to sort by. This key must be present as the 'SortKey' of exactly one 'SortableAttribute' on a property of type T</param>
        /// <param name="direction">The direction to sort (Asc | Desc)</param>
        public static IEnumerable<T> SortByKey<T>(this IEnumerable<T> items, string key, string direction) where T : class
        {
            SortDirection directionEnum = ParseDirection(direction);
            return items.SortByKey<T>(key, directionEnum);
        }

        private static SortDirection ParseDirection(string val)
        {
            SortDirection parsedDirection;
            bool wasParseSuccessful = Enum.TryParse(val, true, out parsedDirection);
            if (wasParseSuccessful == true)
            {
                return parsedDirection;
            }

            throw new ArgumentException($"{val} is not a valid 'SortDirection'");
        }

        private static IEnumerable<T> Sort<T>(IEnumerable<T> items, IEnumerable<SortOperationMetadata> metadata)
        {
            if (metadata.Last().Direction == SortDirection.Asc)
            {
                return items.OrderBy(i => GetNestedValue(metadata, i));
            }

            return items.OrderByDescending(i => GetNestedValue(metadata, i));
        }

        private static object GetNestedValue<T>(IEnumerable<SortOperationMetadata> metadatas, T rootObj)
        {
            Type curType = typeof(T);
            object retVal = rootObj;

            foreach (var metadata in metadatas)
            {
                 retVal = metadata.ToSortBy.GetValue(retVal);
            }

            return retVal;
        }
    }
}
