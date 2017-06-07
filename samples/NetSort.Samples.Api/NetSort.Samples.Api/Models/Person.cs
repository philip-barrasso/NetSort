using System;
namespace NetSort.Samples.Api.Models
{
	public class Person
	{
		[Sortable("age")]
		public int Age { get; set; }

		public string Name { get; set; }
	}
}
