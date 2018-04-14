using System;

namespace BashSoft.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        private const string NotEnrolledInCourse =
            "Student must be enrolled in a course before you set his mark.";

        // Main constructor of the class, using the base one
        public CourseNotFoundException() : base(NotEnrolledInCourse)
        {

        }

        // Secondary constructor of the class, receiving a string and it again uses the base constructor
        public CourseNotFoundException(string message) : base(message)
        {

        }
    }
}
