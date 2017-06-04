using System;
namespace NetSort.Validation
{
    public interface ISortKeyValidator
    {
        /// <summary>
        /// Implementations should check that the 'key' passed in is valid for the type param T
        /// </summary>
        /// <typeparam name="T">The type being validated</typeparam>
        /// <param name="key">The 'SortKey' value of the 'SortableAttribute'<param>
        bool IsKeyValid<T>(string key) where T : class;
    }
}
