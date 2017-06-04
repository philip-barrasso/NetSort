using System;
namespace NetSort.Validation
{
    public interface ISortKeyValidator
    {
        bool IsKeyValid<T>(string key);
    }
}
