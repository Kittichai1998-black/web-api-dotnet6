namespace web_api_dotnet6.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Book> Books { get; set; }
    }
}
