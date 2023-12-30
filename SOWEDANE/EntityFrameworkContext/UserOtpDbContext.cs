using Microsoft.EntityFrameworkCore;
using SOWEDANE.Models;

namespace SOWEDANE.EntityFrameworkContext
{
    public class UserOtpDbContext: DbContext
    {

        public UserOtpDbContext()
        {

        }

        public UserOtpDbContext(DbContextOptions<UserOtpDbContext> options) : base(options)
        {

        }

        public DbSet<UserOtp> UserOtps { get; set; }
    }
}
