using System;
using System.Collections.Generic;

namespace PasswordVerifier
{
    internal class Program
    {
        public static readonly IEnumerable<IRule> Rules = new IRule[]
        {
            new MoreThanEightCharacters(),
            new ContainsAtLeastOneUppercaseCharacter(),
            new ContainsAtLeastOneLowercaseCharacter(),
            new ContainsAtLeastOneNumber()
        };
        private static void Main(string[] args)
        {
            Console.WriteLine("Please Enter a Password");
            new Verifier(Rules, Console.ReadLine()).Verify();
        }
    }
}