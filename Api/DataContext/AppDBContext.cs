using Api.Entitis;
using Microsoft.EntityFrameworkCore;

namespace Api.DataContext
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext>options):base(options)
        {
            
        }
        public AppDBContext()
        {
            
        }
        public DbSet<User> users { get; set; }
        
        
    }
}