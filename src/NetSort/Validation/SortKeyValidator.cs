using System;
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
            SortOperationMetadata metadata = SortOperationMetadataFinder.Find<T>(key);
            return metadata != null;
        }
    }
}
