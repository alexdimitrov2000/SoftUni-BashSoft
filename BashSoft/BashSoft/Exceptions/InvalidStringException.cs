using System;

namespace BashSoft.Exceptions
{
    public class InvalidStringException : Exception
    {
        private const string InvalidString =
            "The value of the variable CANNOT be null or empty!";

        // Main constructor of the class, using the base one
        public InvalidStringException() : base(InvalidString)
        {

        }

        // Secondary constructor of the class, receiving a string and it again uses the base constructor
        public InvalidStringException(string message) : base(message)
        {

        }
    }
}
