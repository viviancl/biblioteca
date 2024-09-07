namespace Biblioteca.Dto
{
    public class TokenDto
    {       
        public required string User { get; set; }
        
        public required string Token { get; set; }

        public int ExpirationTimeHours { get; set; }
    }
}