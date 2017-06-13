using NetSort;
using System;

namespace NetSort.Samples.Api.Models
{
    public class House
    {
        [Sortable("address")]
        public Address Address { get; set; }

        [Sortable("listPrice")]
        public decimal ListPrice { get; set; }
    }
}