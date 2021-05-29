
using Microsoft.EntityFrameworkCore;


namespace VictoryAPI.Models
{
    
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Request> Requests { get; set; }
    }
}
