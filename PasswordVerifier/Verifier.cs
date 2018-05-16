using System.Runtime.Serialization.Formatters;

namespace PasswordVerifier
{
    public class Verifier
    {

        public string Verify(string password)
        {
            var validation = "Invalid";
            if (string.IsNullOrEmpty(password))
                return validation;
            if (password?.Length > 8)
                validation = "Valid";
            return validation;
        }
    }
}
