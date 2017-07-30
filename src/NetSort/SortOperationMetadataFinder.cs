using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetSort
{
    internal class SortOperationMetadataFinder
    {
        public static IEnumerable<SortOperationMetadata> Find<T>(string key, char delimiter, SortDirection? overrideDirection = null) where T : class
        {
            var metadata = new List<SortOperationMetadata>();
            var keyHierarchy = key.Split(delimiter);
            var curType = typeof(T);

            for (var index = 0; index < keyHierarchy.Length; index++)
            {
                var props = curType.GetRuntimeProperties();
                foreach (var prop in props)
                {
                    if (IsSortableProperty(keyHierarchy[index], prop, index == keyHierarchy.Length - 1))
                    {
                        var sortAttribute = prop.GetCustomAttribute<SortableAttribute>();
                        var direction = GetDirection(sortAttribute, overrideDirection);
                        metadata.Add(new SortOperationMetadata(prop, direction));
                        
                        curType = prop.PropertyType;
                        break;
                    }
                }
            }
            
            return metadata;
        }

        private static bool IsSortableProperty(string key, PropertyInfo prop, bool isLeafProperty)
        {
            var sortAttribute = prop.GetCustomAttribute<SortableAttribute>();
            return sortAttribute != null && 
                sortAttribute.SortKey == key && 
                (isLeafProperty == false || IsIComparable(prop) == true);
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
