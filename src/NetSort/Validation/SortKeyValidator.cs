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
            int expectedMetadatasCount = key.Split('.').Length;

            IEnumerable<SortOperationMetadata> metadatas = SortOperationMetadataFinder.Find<T>(key);
            return metadatas.Count() == expectedMetadatasCount;
        }
    }
}
