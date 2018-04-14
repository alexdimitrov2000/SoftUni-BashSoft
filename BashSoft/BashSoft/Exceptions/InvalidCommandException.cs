using System;

namespace BashSoft.Exceptions
{
    public class InvalidCommandException : Exception
    {
        private const string InvalidCommand = "The command '{0}' is invalid";
        // Main constructor of the class, receiving a string and uses the base constructor
        public InvalidCommandException(string input) : base(string.Format(InvalidCommand, input))
        {
        }

        //public InvalidCommandException(string message) : base(message)
        //{

        //}
    }
}
