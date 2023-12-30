using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOWEDANE.Models;

namespace SOWEDANE.EntityFrameworkContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<UserOtp> UserOtps { get; set; }


    }
}
