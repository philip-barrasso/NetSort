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
        public async Task<IActionResult> GetPeople([FromQuery]string sortBy, [FromQuery]string sortDir = null)
		{
            if (_sortKeyValidator.IsKeyValid<Person>(sortBy) == false)
			{
				return BadRequest($"{sortBy} is not a valid key for sorting people");
			}

            var people = new List<Person>()
			{
				new Person() 
                { 
                    Age = 15, 
                    Name = "Bob", 
                    House = new House() 
                    { 
                        ListPrice = 15, 
                        Address = new Address()
                        {
                            City = "Atlanta",
                            State = "GA",
                            Zip = "30308",
                            Street = "Peachtree",
                        }
                    } 
                },
                new Person() 
                { 
                    Age = 8, 
                    Name = "Steve", 
                    House = new House() 
                    { 
                        ListPrice = 18, 
                        Address = new Address()
                        {
                            City = "Kansas City",
                            State = "KS",
                            Zip = "12388",
                            Street = "Main",
                        }
                    } 
                },
                new Person() 
                { 
                    Age = 11, 
                    Name = "Sally", 
                    House = new House() 
                    { 
                        ListPrice = 5, 
                        Address = new Address()
                        {
                            City = "Memphis",
                            State = "TN",
                            Zip = "31214",
                            Street = "Beale",
                        }
                    } 
                },
			};

			if (sortDir != null)
			{
				return Ok(people.SortByKey(sortBy, sortDir));
			}

			return Ok(people.SortByKey(sortBy));
		}
	}
}
