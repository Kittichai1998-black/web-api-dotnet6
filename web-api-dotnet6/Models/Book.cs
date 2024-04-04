namespace web_api_dotnet6.Models
{
    public class Book
    {
        public int Id {  get; set; }
        public string Title {  get; set; }
        public string BookType { get; set; }
        public decimal Price { get; set; }
        public Author Author { get; set; }

    }
}
