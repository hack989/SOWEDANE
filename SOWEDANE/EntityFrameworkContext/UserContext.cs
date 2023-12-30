using Microsoft.EntityFrameworkCore;
using SOWEDANE.Models;

namespace SOWEDANE.EntityFrameworkContext
{
    public class UserContext:DbContext
    {
        public UserContext()
        {

        }

        public UserContext(DbContextOptions<UserContext> options):base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }


    }
}
