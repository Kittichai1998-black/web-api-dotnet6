using web_api_dotnet6.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System;
namespace web_api_dotnet6
{
    public class ApiContext : DbContext
    {
        //protected override void OnConfiguring
        //    (DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
        //}
        public ApiContext(DbContextOptions<DbContext> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<FreeAPI> FreeAPIs { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FreeAPI>().HasNoKey();
            modelBuilder.Entity<Response>().HasNoKey();
        }


    }
}