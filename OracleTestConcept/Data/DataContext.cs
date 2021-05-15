using Microsoft.EntityFrameworkCore;

namespace OracleTestConcept.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }
    }
}
