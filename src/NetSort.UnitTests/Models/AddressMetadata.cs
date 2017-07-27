using System;
using System.Collections.Generic;
using System.Text;

namespace NetSort.UnitTests.Models
{
    public class AddressMetadata
    {
        public string SomeField { get; set; }

        [Sortable("decimal")]
        public decimal SomeDecimal { get; set; }
    }
}
