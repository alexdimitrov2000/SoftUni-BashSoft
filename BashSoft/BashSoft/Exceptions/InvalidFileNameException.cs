using System;

namespace BashSoft.Exceptions
{
    public class InvalidFileNameException : Exception
    {
        private const string ForbiddenSymbolsContainedInName =
            "The given name contains symbols that are not allowed to be used in names of files and folders";

        // Main constructor of the class, using the base one
        public InvalidFileNameException() : base(ForbiddenSymbolsContainedInName)
        {

        }

        // Secondary constructor of the class, receiving a string and it again uses the base constructor
        public InvalidFileNameException(string message) : base(message)
        {

        }
    }
}
