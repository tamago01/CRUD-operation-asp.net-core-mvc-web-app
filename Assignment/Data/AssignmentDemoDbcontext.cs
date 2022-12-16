using Assignment.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Data
{
    public class AssignmentDemoDbcontext : DbContext
    {
        public AssignmentDemoDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Consumer> Consumers { get; set; }
    }


}
