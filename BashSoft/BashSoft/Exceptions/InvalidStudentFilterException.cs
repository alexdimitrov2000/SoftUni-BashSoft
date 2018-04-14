using System;

namespace BashSoft.Exceptions
{
    public class InvalidStudentFilterException : Exception
    {
        private const string InvalidStudentFilter = "The given filter is not one of the following: excellent/average/poor";

        // Main constructor of the class, using the base one
        public InvalidStudentFilterException() : base(InvalidStudentFilter)
        {

        }

        // Secondary constructor of the class, receiving a string and it again uses the base constructor
        public InvalidStudentFilterException(string message) : base(message)
        {

        }
    }
}
