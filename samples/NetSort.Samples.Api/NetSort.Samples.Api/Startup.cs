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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<InMemoryDbContext>();
            SeedDb(context);

            app.UseMvc();
        }

        private void SeedDb(InMemoryDbContext ctx)
        {
            var persons = new List<Person>
            {
                new Person { Id = 1, Age = 25, Name = "Bob Smith" },
                new Person { Id = 2, Age = 28, Name = "Angie Blarn" },
                new Person { Id = 3, Age = 26, Name = "Bob Sith" },
                new Person { Id = 4, Age = 25, Name = "David Blue" },
            };

            ctx.Persons.AddRange(persons);
            
            var addresses = new List<Address>
            {
                new Address { Id = 1, City = "Atlanta", State = "GA", Street = "1234 Peachtree St", Zip = "30308" },
                new Address { Id = 2, City = "Atlanta", State = "GA", Street = "111 North St", Zip = "30308" },
                new Address { Id = 3, City = "Atlanta", State = "GA", Street = "5555 W. Peachtree St", Zip = "30308" }
            };

            ctx.Addresses.AddRange(addresses);

            var houses = new List<House>
            {
                new House { Id = 1, AddressId = 1, OwnerId = 1, ListPrice = 150000M },
                new House { Id = 2, AddressId = 2, OwnerId = 2, ListPrice = 250000M },
                new House { Id = 3, AddressId = 3, OwnerId = 3, ListPrice = 175000M },
            };

            ctx.Houses.AddRange(houses);

            ctx.SaveChanges();
        }
    }
}
