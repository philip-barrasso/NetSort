using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetSort.Validation;
using Microsoft.EntityFrameworkCore;
using NetSort.Samples.Api.Models;

namespace NetSort.Samples.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<ISortKeyValidator, SortKeyValidator>();

            // Add framework services.
            services.AddMvc();

            services.AddDbContext<InMemoryDbContext>(opt => opt.UseInMemoryDatabase());

            var connection = @"Server=(localdb)\mssqllocaldb;Database=NetSortSampleDb;Trusted_Connection=True;";
            services.AddDbContext<PersistentDbContext>(opt => opt.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var inMemoryContext = app.ApplicationServices.GetService<InMemoryDbContext>();
            SeedDb(inMemoryContext);

            var persistentContext = app.ApplicationServices.GetService<PersistentDbContext>();
            if (!persistentContext.Persons.Any())
            {
                SeedDb(persistentContext);
            }           

            app.UseMvc();
        }
               
        private void SeedDb(InMemoryDbContext ctx)
        {
            var persons = GetPersonsToSeed();
            ctx.Persons.AddRange(persons);

            var addresses = GetAddressesToSeed(); 
            ctx.Addresses.AddRange(addresses);

            var houses = GetHousesToSeed();
            ctx.Houses.AddRange(houses);

            ctx.SaveChanges();
        }

        private void SeedDb(PersistentDbContext ctx)
        {
            var houses = new List<House>
            {
                new House
                {
                    Address = new Address
                    {
                        City = "Atlanta",
                        State = "GA",
                        Street = "123 Peachtree St",
                        Zip = "30308",
                    },
                    Owner = new Person
                    {
                        Age = 25,
                        Name = "Bob Smith",
                    },
                },
                new House
                {
                    Address = new Address
                    {
                        City = "Atlanta",
                        State = "GA",
                        Street = "555 North St",
                        Zip = "30308",
                    },
                    Owner = new Person
                    {
                        Age = 25,
                        Name = "Sally Green",
                    },
                },
                new House
                {
                    Address = new Address
                    {
                        City = "Atlanta",
                        State = "GA",
                        Street = "009 Peachtree St",
                        Zip = "30308",
                    },
                    Owner = new Person
                    {
                        Age = 21,
                        Name = "Bob Smith",
                    },
                },
                new House
                {
                    Address = new Address
                    {
                        City = "Atlanta",
                        State = "GA",
                        Street = "123 North St",
                        Zip = "30308",
                    },
                    Owner = new Person
                    {
                        Age = 33,
                        Name = "Danny Blue",
                    },
                },
            };

            ctx.Houses.AddRange(houses);
            ctx.SaveChanges();
        }

        private List<Person> GetPersonsToSeed()
        {
            return new List<Person>
            {
                new Person { Id = 1, Age = 25, Name = "Bob Smith" },
                new Person { Id = 2, Age = 28, Name = "Angie Blarn" },
                new Person { Id = 3, Age = 26, Name = "Bob Sith" },
                new Person { Id = 4, Age = 25, Name = "David Blue" },
            };
        }

        private List<House> GetHousesToSeed()
        {
            return new List<House>
            {
                new House { Id = 1, AddressId = 1, OwnerId = 1, ListPrice = 150000M },
                new House { Id = 2, AddressId = 2, OwnerId = 2, ListPrice = 250000M },
                new House { Id = 3, AddressId = 3, OwnerId = 3, ListPrice = 175000M },
            };
        }

        private List<Address> GetAddressesToSeed()
        {
            return new List<Address>
            {
                new Address { Id = 1, City = "Atlanta", State = "GA", Street = "1234 Peachtree St", Zip = "30308" },
                new Address { Id = 2, City = "Atlanta", State = "GA", Street = "111 North St", Zip = "30308" },
                new Address { Id = 3, City = "Atlanta", State = "GA", Street = "5555 W. Peachtree St", Zip = "30308" }
            };
        }
    }
}
