using System;

namespace PasswordVerifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var password = !Console.ReadLine().Equals("") ? Console.ReadLine() : null;
            Console.WriteLine("Please Enter a Password");
            Console.WriteLine("Verifying");
            Console.WriteLine(new Verifier().Verify(password));
        }
    }
}
