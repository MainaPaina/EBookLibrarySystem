using Microsoft.EntityFrameworkCore;
using Book_Library_Management_System.Models.Entities;
using Book_Library_Management_System.Data;

namespace Book_Library_Management_System.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Rating> Ratings { get; set; }

    }
}
