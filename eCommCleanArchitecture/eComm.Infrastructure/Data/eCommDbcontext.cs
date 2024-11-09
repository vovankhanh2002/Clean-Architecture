using eComm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace eComm.Infrastructure.Data
{
    public class eCommDbcontext: DbContext
    {
        public eCommDbcontext(DbContextOptions<eCommDbcontext> options) : base(options) { }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
