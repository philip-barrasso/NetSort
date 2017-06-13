using NetSort;

namespace NetSort.Samples.Api.Models
{
    public class House
    {
        public Address Address { get; set; }

        [Sortable("listPrice")]
        public decimal ListPrice { get; set; }
    }
}