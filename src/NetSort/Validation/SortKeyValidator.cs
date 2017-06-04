using System;
namespace NetSort.Validation
{
    public class SortKeyValidator : ISortKeyValidator
    {
        public bool IsKeyValid<T>(string key)
        {
            SortOperationMetadata metadata = SortOperationMetadataFinder.Find<T>(key);
            return metadata != null;
        }
    }
}
