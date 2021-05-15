using System;
using System.Linq;
using System.Threading.Tasks;

namespace OracleTestConcept.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            try
            {
                await _context.Database.EnsureCreatedAsync();
                await SeedDataAync();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private async Task SeedDataAync()
        {
            if (!_context.Tests.Any())
            {
                _context.Tests.Add(new Test { Name = "Test 1" });
                _context.Tests.Add(new Test { Name = "Test 2" });
                _context.Tests.Add(new Test { Name = "Test 3" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
