using web_api_dotnet6.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace web_api_dotnet6
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly ApiContext _context;

        public AuthorRepository(ApiContext context)
        {
            _context = context;
        }
        public AuthorRepository()
        {

            using (var context = new ApiContext())
            {
                if (context.Authors != null && context.Books != null)
                {
                    context.RemoveRange(context.Authors);
                    context.RemoveRange(context.Books);
                }

                var authors = new List<Author>
                {

                new Author
                {
                    UserName ="Kittichai",
                    Email ="kittichai.n1998@gmail.com",
                       Books = new List<Book>()
                    {
                        new Book { 
                            Id = 1,
                            Title = "ASP.NET Core MVC .NET6",
                            BookType = "IT",
                            Price = 395
                            
                        },
                        new Book {
                            Id = 2,
                            Title = "Next.JS + Node.JS",
                            BookType = "IT",
                            Price = 289

                        },
                        new Book {
                            Id = 3,
                            Title = "English 3000",
                            BookType = "Foreign Language",
                            Price = 359

                        },
                        new Book {
                            Id = 4,
                            Title = "Rich father teaches his children",
                            BookType = "Investments",
                            Price = 300

                        },

                    }
                },
                new Author
                {
                    UserName ="Kittichai2",
                    Email ="kittichai.n1999@gmail.com",
                    Books = new List<Book>()
                    {
                         new Book {
                            Id = 5,
                            Title = "Learn cat habits",
                            BookType = "General knowledge",
                            Price = 189
                        },
                        new Book {
                            Id = 6,
                            Title = "Know about taxes",
                            BookType = "Law",
                            Price = 299

                        },
                    }
                }
                };
                context.Authors.AddRange(authors);
                context.SaveChanges();

            }
        }
        public string GetAuthors()
        {
            using (var context = new ApiContext())
            {
                var list = context.Authors
                    .Include(a => a.Books)
                    .ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                string jsonString = JsonSerializer.Serialize(list, options);
                return jsonString;
            }
        }

        public void ClearData()
        {
            // ลบข้อมูลทั้งหมดใน DbSet<Author>
            _context.Authors.RemoveRange(_context.Authors) ;

            // ลบข้อมูลทั้งหมดใน DbSet<Book>
            _context.Books.RemoveRange(_context.Books);

            // SaveChanges เพื่อยืนยันการลบข้อมูล
            _context.SaveChanges();
        }

    }
}
