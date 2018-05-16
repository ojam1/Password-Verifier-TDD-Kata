namespace PasswordVerifier
{
    public class Verifier
    {
        public string Verify(string password)
        {
            if (password.Length > 8)
                return "Valid";
            return "Invalid";
        }
    }
}
