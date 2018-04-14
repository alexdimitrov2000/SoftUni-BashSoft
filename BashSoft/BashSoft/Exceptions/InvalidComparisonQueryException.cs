using System;

namespace BashSoft.Exceptions
{
    public class InvalidComparisonQueryException : Exception
    {
        private const string InvalidComparisonQuery =
            "The comparison query you want, does not exist in the context of the current program!";

        // Main constructor of the class, using the base one
        public InvalidComparisonQueryException() : base(InvalidComparisonQuery)
        {

        }

        // Secondary constructor of the class, receiving a string and it again uses the base constructor
        public InvalidComparisonQueryException(string message) : base(message)
        {

        }
    }
}
