using Microsoft.EntityFrameworkCore;

namespace FluentValidationApp.Models
{
    /// <summary>
    /// Database ile bağlantı kuracak class
    /// Database ile ilgili işlemler burada gerçekleşitirilir.
    /// </summary>
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        // Model class ile Database tablosunu birbiri ile ilişkilendirdik.
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
