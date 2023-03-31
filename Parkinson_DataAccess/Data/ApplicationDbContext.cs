using Microsoft.EntityFrameworkCore;
using Parkinson_Models;

namespace Parkinson_DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Test1> Test1s { get; set; }

    }
}
