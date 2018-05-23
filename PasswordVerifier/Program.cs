using System;

namespace PasswordVerifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter a Password");
            Console.WriteLine(new Verifier(null).Verify());
        }
    }
}
