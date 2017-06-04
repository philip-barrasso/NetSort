using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetSort
{
    internal class SortOperationMetadataFinder
    {
        public static SortOperationMetadata Find<T>(string key, SortDirection? overrideDirection = null) where T : class
		{
			IEnumerable<PropertyInfo> props = typeof(T).GetRuntimeProperties();
			foreach (var prop in props)
			{
				var sortAttribute = prop.GetCustomAttribute<SortableAttribute>();
                if (sortAttribute != null && sortAttribute.SortKey == key && IsIComparable(prop) == true)
				{
					SortDirection direction = GetDirection(sortAttribute, overrideDirection);
					return new SortOperationMetadata(prop, direction);
				}
			}

			return null;
		}

        private static bool IsIComparable(PropertyInfo prop)
        {
            return typeof(IComparable).GetTypeInfo().IsAssignableFrom(prop.PropertyType.GetTypeInfo());  
        }

		private static SortDirection GetDirection(SortableAttribute attribute, SortDirection? overrideVal)
		{
			if (overrideVal != null)
			{
				return overrideVal.Value;
			}

			if (attribute != null && attribute.DefaultSortDirection != null)
			{
				return attribute.DefaultSortDirection.Value;
			}

			return Constants.DEFAULT_SORT_DIRECTION;
		} 
    }
}
