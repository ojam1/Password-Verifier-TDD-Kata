using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;

namespace PasswordVerifier
{
    public class Verifier
    {
        private readonly Dictionary<IRule, bool> _conditions;
 
        public Verifier(IEnumerable<IRule> rules, string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new IncorrectPassword("Password is null or empty");

            _conditions = new Dictionary<IRule, bool>();
            

            foreach (var rule in rules)
            {
                _conditions.Add(rule, rule.ConditionMet(password));
            }
        }

        public string Verify()
        {
            ErrorMessage();
           return Validation();
        }

        private string Validation()
        {
            return ConditionChecker() == 4 ? "Valid" : "Invalid";
        }

        private int ConditionChecker()
        {
            return _conditions.Values.Count(value => value.Equals(true));
        }

        private void ErrorMessage()
        {
            var key = _conditions.Where(failingRule => failingRule.Value == false)
                .Select(failingRule => failingRule.Key).ToList();

            if(key.Any())
                throw new IncorrectPassword(key.First().ExceptionMessage);
        }
    }

    public class ContainsAtLeastOneUppercaseCharacter : IRule
    {
        public string ExceptionMessage { get; }

        public ContainsAtLeastOneUppercaseCharacter()
        {
            ExceptionMessage = "Password should contain at least one uppercase character";
        }

        public bool ConditionMet(string password)
        {
            return password.Any(char.IsUpper);
        }
    }

    public class ContainsAtLeastOneLowercaseCharacter : IRule
    {
        public string ExceptionMessage { get; }

        public ContainsAtLeastOneLowercaseCharacter()
        {
            ExceptionMessage = "Password should contain at least one lowercase character";
        }

        public bool ConditionMet(string password)
        {
            return password.Any(char.IsLower);
        }
    }

    public class ContainsAtLeastOneNumber : IRule
    {
        public string ExceptionMessage { get; }

        public ContainsAtLeastOneNumber()
        {
            ExceptionMessage = "Password should contain at least one number";
        }

        public bool ConditionMet(string password)
        {
            return password.Any(char.IsNumber);
        }
    }
    public class MoreThanEightCharacters : IRule
    {
        public string ExceptionMessage { get; }

        public MoreThanEightCharacters()
        {
            ExceptionMessage = "Password should be at least 8 characters";
        }

        public bool ConditionMet(string password)
        {
            return password.Length > 8;
        }
    }

    public interface IRule
    {
        string ExceptionMessage { get; }
        bool ConditionMet(string conditionToTest);
    }
}