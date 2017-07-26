using System;
using System.Collections.Generic;
using System.Linq;

namespace NetSort.Validation
{
    public class SortKeyValidator : ISortKeyValidator
    {
        /// <summary>
        /// Ensure that the type 'T' has a 'SortableAttribute' on a property identified by the 'key'
        /// </summary>
        /// <typeparam name="T">The type being validated</typeparam>
        /// <param name="key">The 'SortKey' value of the 'SortableAttribute'<param>
        public bool IsKeyValid<T>(string key) where T : class
        {
            return IsKeyValid<T>(key, Constants.DEFAULT_NESTED_PROP_DELIMITER);
        }

        /// <summary>
        /// Ensure that the type 'T' has a 'SortableAttribute' on a property identified by the 'key'
        /// </summary>
        /// <typeparam name="T">The type being validated</typeparam>
        /// <param name="key">The 'SortKey' value of the 'SortableAttribute'<param>
        /// <param name="delimiter">The char used to separate nested properties</param>
        public bool IsKeyValid<T>(string key, char delimiter) where T : class
        {
            var expectedMetadatasCount = key.Split(delimiter).Length;
            var metadatas = SortOperationMetadataFinder.Find<T>(key, delimiter);

            return metadatas.Count() == expectedMetadatasCount;
        }
    }
}
