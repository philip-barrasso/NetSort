using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using NetSort.Validation;
using System.Threading.Tasks;
using NetSort.Samples.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetSort.Samples.Api.Controllers
{
    [Route("api/v1/people")]
    public class PeopleController : Controller
	{
        private readonly ISortKeyValidator _sortKeyValidator;
        private readonly InMemoryDbContext _inMemoryDbContext;
        private readonly PersistentDbContext _dbContext;

		public PeopleController(
            ISortKeyValidator sortKeyValidator, 
            InMemoryDbContext inMemoryDbContext,
            PersistentDbContext dbContext)
		{
			_sortKeyValidator = sortKeyValidator;
            _inMemoryDbContext = inMemoryDbContext;
            _dbContext = dbContext;
		}

		[HttpGet]
		[Route("inmemory")]
        public async Task<IActionResult> GetInMemoryPeople([FromQuery]string sortBy = null, [FromQuery]string sortDir = null, 
                                                   [FromQuery]string thenSortBy = null, [FromQuery]string thenSortByDir = null)
		{
            if (_sortKeyValidator.IsKeyValid<Person>(sortBy) == false)
			{
				return BadRequest($"{sortBy} is not a valid key for sorting people");
			}

            var persons = _inMemoryDbContext.Persons
                .SortByKey(sortBy, sortDir ?? "asc")
                .ThenSortByKey(thenSortBy, thenSortByDir ?? "asc");

            return Ok(persons);
		}

        [HttpGet]
        [Route("fromdb")]
        public async Task<IActionResult> GetPeopleFromDb([FromQuery]string sortBy = null, [FromQuery]string sortDir = null,
                                                   [FromQuery]string thenSortBy = null, [FromQuery]string thenSortByDir = null)
        {
            if (_sortKeyValidator.IsKeyValid<Person>(sortBy) == false)
            {
                return BadRequest($"{sortBy} is not a valid key for sorting people");
            }

            var persons = _dbContext.Persons
                .SortByKey(sortBy, sortDir ?? "asc")
                .ThenSortByKey(thenSortBy, thenSortByDir ?? "asc");

            return Ok(persons);
        }
    }
}
