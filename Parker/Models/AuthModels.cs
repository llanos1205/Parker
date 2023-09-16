namespace Parker.Models;

public class AuthModels
{
    public class TokenModel
    {
        public string Access { get; set; }
        public string Refresh { get; set; }
    }

    public class CredentialsModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
}