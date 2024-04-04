using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_dotnet6.Models
{
    public class FreeAPI
    {
        public string url { get; set; }
        public string method { get; set; }
        [NotMapped]
        public Response response { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public decimal probability { get; set; }
    }
}
