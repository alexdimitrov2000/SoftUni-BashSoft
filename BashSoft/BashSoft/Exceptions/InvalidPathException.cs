using System;

namespace BashSoft.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string InvalidPath = "The source does not exist.";

        // Main constructor of the class, using the base one
        public InvalidPathException() : base(InvalidPath)
        {

        }

        // Secondary constructor of the class, receiving a string and it again uses the base constructor
        public InvalidPathException(string message) : base(message)
        {

        }
    }
}
