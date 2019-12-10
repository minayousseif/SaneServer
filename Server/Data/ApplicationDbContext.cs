using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaneServer.Server.Models;

namespace SaneServer.Server.Data
{
    /// <summary>
    /// The DB connection context object for the application
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            // Seeding the database with a default admin creds
            string UserName = "admin";
            modelBuilder.Entity<User>().HasData(
               new User {
                   Id = 1,
                   UserName = UserName,
                   PasswordHash = new PasswordHasher<string>().HashPassword(UserName, "admin"),
                   Role = UserRole.Admin
                }
            );
        }    
        
        public DbSet<User> Users { get; set; }
        public DbSet<Scanner> Scanners { get; set; }
    }
}