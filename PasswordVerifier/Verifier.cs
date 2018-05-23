using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;

namespace PasswordVerifier
{
    public class Verifier
    {
        private Password _password;
        private int NumberOfPassedConditions { get; set; }
        
        public Verifier(string password)
        {   
            if (string.IsNullOrEmpty(password))
                throw new IncorrectPassword("Password is null or empty");

            _password = new Password(password);
            NumberOfPassedConditions = 1;
        }
        public string Verify()
        {
            if (_password.ActualPassword.Length < 8)
                _password.IsLessThan8CharPassword = true;

            NumberOfPassedConditions++;

            foreach (var @char in _password.ActualPassword)
            {
                if (char.IsUpper(@char))
                {
                    _password.ContainAtLeast1UppercaseChar = true;
                    NumberOfPassedConditions++;
                }
            }

            if (NumberOfPassedConditions == 3)
                return "Valid";

            foreach (var @char in _password.ActualPassword)
            {
                if (char.IsLower(@char))
                    _password.ContainsAtLeast1LowercaseChar = true;
            }

            foreach (var @char in _password.ActualPassword)
            {
                if (int.TryParse(@char.ToString(), out _))
                    _password.ContainsAtLeast1Number = true;
            }

            if (AllConditionsMet())
                return "Valid";

            ErrorMessage();
            return "Invlaid";
        }

        private bool AllConditionsMet()
        {
            return ConditionsThatShouldBeTrue() && ConditionsThatShouldBeFalse();
        }

        private bool ConditionsThatShouldBeTrue()
        {
            return _password.ContainsAtLeast1LowercaseChar && _password.ContainAtLeast1UppercaseChar && _password.ContainsAtLeast1Number;
        }

        private bool ConditionsThatShouldBeFalse()
        {
            return !_password.IsLessThan8CharPassword;
        }
        private void ErrorMessage()
        {
            if (_password.IsLessThan8CharPassword)
                throw new IncorrectPassword("Password should be at least 8 characters");

            if (!_password.ContainAtLeast1UppercaseChar)
                throw new IncorrectPassword("Password should contain at least one uppercase character");

            if (!_password.ContainsAtLeast1LowercaseChar)
                throw new IncorrectPassword("Password should contain at least one lowercase character");

            if (!_password.ContainsAtLeast1Number)
                throw new IncorrectPassword("Password should contain at least one number");
        }
    }

    public class Password
    {
        public string ActualPassword;
        public bool IsLessThan8CharPassword;
        public bool ContainsAtLeast1LowercaseChar;
        public bool ContainAtLeast1UppercaseChar;
        public bool ContainsAtLeast1Number;

        public Password(string password)
        {
            ActualPassword = password;
            IsLessThan8CharPassword = false;
            ContainsAtLeast1LowercaseChar = false;
            ContainAtLeast1UppercaseChar = false;
            ContainsAtLeast1Number = false;
        }
    }

    [Serializable]
    public class IncorrectPassword : Exception
    {
        public IncorrectPassword()
        {
        }

        public IncorrectPassword(string message) : base(message)
        {
        }

        public IncorrectPassword(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectPassword(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
