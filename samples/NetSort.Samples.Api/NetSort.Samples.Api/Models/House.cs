using NetSort;
using System;

namespace NetSort.Samples.Api.Models
{
    public class House
    {
        public long Id { get; set; }

        [Sortable("listPrice")]
        public decimal ListPrice { get; set; }

        public long OwnerId { get; set; }

        public Person Owner { get; set; }
        
        public long AddressId { get; set; }

        public Address Address { get; set; }
    }
}