namespace Identity.API.Models
{
    public class TokenGenerationRequest
    {
        public string Email  { get; set; }
        public string Password { get; set; }
    }
}
