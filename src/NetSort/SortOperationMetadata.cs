using System.Reflection;

namespace NetSort
{
    internal class SortOperationMetadata
    {
        public PropertyInfo ToSortBy { get; private set; }

        public SortDirection Direction { get; private set; }

        public SortOperationMetadata(PropertyInfo toSortBy, SortDirection direction)
        {
            ToSortBy = toSortBy;
            Direction = direction;
        }
    }
}
