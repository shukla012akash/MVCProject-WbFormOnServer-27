using Microsoft.EntityFrameworkCore;
using Sample27.Models;

namespace Sample27.Data
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<EmployeeData>Employees { get; set; }
    }
}
