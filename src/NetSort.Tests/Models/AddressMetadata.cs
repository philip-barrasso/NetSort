using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSort.Tests.Models
{
    public class AddressMetadata
    {
        public string SomeField { get; set; }

        [Sortable("decimal")]
        public decimal SomeDecimal { get; set; }
    }
}
