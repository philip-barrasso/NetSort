using System;
namespace NetSort
{
    public class SortableAttribute : Attribute
    {
        public string SortKey { get; private set; }

        public SortDirection? DefaultSortDirection { get; private set; }

        public SortableAttribute(string sortKey)
        {
            SortKey = sortKey;
        }

        public SortableAttribute(string sortKey, SortDirection defaultSortDirection)
        {
            SortKey = sortKey;
            DefaultSortDirection = defaultSortDirection;
        }
    }
}
