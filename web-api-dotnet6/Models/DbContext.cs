using web_api_dotnet6.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace web_api_dotnet6.Models
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FreeAPI> FreeAPIs { get; set; }
        public DbSet<Response> Responses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FreeAPI>().HasNoKey();
            modelBuilder.Entity<Response>().HasNoKey();
        }


    }
}