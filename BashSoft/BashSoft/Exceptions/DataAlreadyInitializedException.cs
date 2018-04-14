using System;

namespace BashSoft.Exceptions
{
    public class DataAlreadyInitializedException : Exception
    {
        private const string DataAlreadyInitialisedException =
            "Data is already intialized!";

        // Main costructor, which does not accept any parameters and uses the base one
        public DataAlreadyInitializedException() : base(DataAlreadyInitialisedException)
        {

        }

        // Secondary constructor, which receives a string parameter and again uses the base constructor
        public DataAlreadyInitializedException(string message) : base(message)
        {

        }
    }
}
