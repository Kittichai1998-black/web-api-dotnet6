using Microsoft.EntityFrameworkCore;
using web_api_dotnet6.Models;

namespace web_api_dotnet6.Data
{
   public class AppDbContext : DbContext
   {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            // ตัวอย่างการสร้าง DbSet สำหรับ Entity
      public DbSet<User> Users { get; set; }
   }
}
