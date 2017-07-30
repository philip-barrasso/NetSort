using System;
using NetSort;

namespace NetSort.Samples.Api.Models
{
	public class Person
	{
        public long Id { get; set; }

		[Sortable("age")]
		public int Age { get; set; }

        [Sortable("name")]
		public string Name { get; set; }
	}
}
