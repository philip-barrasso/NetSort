# NetSort

[![Build status](https://ci.appveyor.com/api/projects/status/qe0cobkgt70jr2bu/branch/master?svg=true)](https://ci.appveyor.com/project/philip-barrasso/netsort/branch/master) ![#](https://img.shields.io/nuget/v/NetSort.svg)

# Example

## Sort the response of an HTTP GET for people by their age

Class to be sorted
```
public class Person
{
    [Sortable("age")]
    public int Age { get; set; }

    public string Name { get; set; }
}
```

Controller (dotnet core)
```
[Route("api/v1/people")]
public class PeopleController : Controller
{
    private ISortKeyValidator _sortKeyValidator;

    public PeopleController(ISortKeyValidator sortKeyValidator)
    {
        _sortKeyValidator = sortKeyValidator;
    }

    [HttpGet]
    [Route("api/v1/people")]
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
```

200 Request ==> http://BASE_URL/api/v1/people?sortBy=age <br />
400 Request ==> http://BASE_URL/api/v1/people?sortBy=age3

# Installation

Nuget:

```
PM> Install-Package NetSort
```