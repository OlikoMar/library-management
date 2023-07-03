namespace Identity.Data.Entities;

public class RefreshToken
{
    public string Token { get; set; }
    public string UserId { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
}