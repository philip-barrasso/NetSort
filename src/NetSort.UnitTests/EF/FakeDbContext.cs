using Microsoft.EntityFrameworkCore;

namespace NetSort.UnitTests.EF
{
    public class FakeDbContext : DbContext
    {
        public virtual DbSet<EF_Person> EF_Persons { get; set; }
    }
}
