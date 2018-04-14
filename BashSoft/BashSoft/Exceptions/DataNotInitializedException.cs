using System;

namespace BashSoft.Exceptions
{
    public class DataNotInitializedException : Exception
    {
        private const string DataNotInitializedExceptionMessage =
            "The data structure must be initialized first in order to make any operation with it.";

        // Main constructor of the class, using the base one
        public DataNotInitializedException() : base(DataNotInitializedExceptionMessage)
        {

        }

        // Secondary constructor of the class, receiving a string and it again uses the base constructor
        public DataNotInitializedException(string message) : base(message)
        {

        }
    }
}
