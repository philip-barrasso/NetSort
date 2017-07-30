using System;

namespace NetSort
{
    /// <summary>
    /// An attribute that's used to decorate properties in classes that should be sortable. IEnumerable.SortByKey will throw an exception if the 'key' passed in as a parameter does not correspond with a 'SortableAttribute'
    ///</summary>
    public class SortableAttribute : Attribute
    {
        /// <summary>
        /// The 'key' that can be used to sort by this property
        /// </summary>
        public string SortKey { get; private set; }

        /// <summary>
        /// A default sort direction that can optionally be set for this property
        /// </summary>
        public SortDirection? DefaultSortDirection { get; private set; }

        /// <summary>
        /// To be used when a property should not specify a default sort order
        /// </summary>
        public SortableAttribute(string sortKey)
        {
            SortKey = sortKey;
        }

        /// <summary>
        /// To be used when a property should specify a default sort order
        /// </summary>
        public SortableAttribute(string sortKey, SortDirection defaultSortDirection)
        {
            SortKey = sortKey;
            DefaultSortDirection = defaultSortDirection;
        }
    }
}
