namespace WebShop       
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate (string username, string password);
    }
}
