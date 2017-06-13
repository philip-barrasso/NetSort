using System;
using NetSort;

namespace NetSort.Samples.Api.Models
{
	public class Person
	{
		[Sortable("age")]
		public int Age { get; set; }

		public string Name { get; set; }

        [Sortable("house")]
        public House House { get; set; }
	}
}
