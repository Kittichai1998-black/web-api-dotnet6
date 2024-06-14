namespace web_api_dotnet6.Models
{
    public class User
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Firstname { get; set; }
        public string lastname { get; set; }
        public string Fullname { get; set; }
        public string Cardno { get; set; }
        public string Birthdate { get; set; }
        public string IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
