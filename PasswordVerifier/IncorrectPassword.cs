using System;
using System.Runtime.Serialization;

namespace PasswordVerifier
{
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
