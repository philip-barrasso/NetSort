using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using NetSort.Validation;
using System.Threading.Tasks;
using NetSort.Samples.Api.Models;
using System.Collections.Generic;
namespace NetSort.Samples.Api.Controllers
{
    [Route("api/v1/people")]
    public class PeopleController : Controller
	{
        private ISortKeyValidator _sortKeyValidator;

		public PeopleController(ISortKeyValidator sortKeyValidator)
		{
			_sortKeyValidator = sortKeyValidator;
		}

		[HttpGet]
		[Route("")]
        public async Task<IActionResult> GetPeople([FromQuery]string sortBy)
		{
            if (_sortKeyValidator.IsKeyValid<Person>(sortBy) == false)
			{
				return BadRequest($"{sortBy} is not a valid key for sorting people");
			}

            var people = new List<Person>()
			{
				new Person() { Age = 15, Name = "Bob" },
				new Person() { Age = 8, Name = "Steve" },
				new Person() { Age = 11, Name = "Sally" },
			};

			var sortedPeople = people.SortByKey(sortBy);
			return Ok(sortedPeople);
		}
	}
}
