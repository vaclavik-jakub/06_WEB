using Microsoft.EntityFrameworkCore;
using _06_WEB.Models.DataModels;

namespace _06_WEB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ImageFolder> ImageFolders { get; set; }
    }
}
