using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("show")]
    public class ShowCourseCommand : Command
    {
        [Inject]
        private IDatabase repository;

        // Main constructor of the class which receives all the parameters the base class needs
        public ShowCourseCommand(string input, string[] data) : base(input, data)
        {

        }

        // As the main "Execute" method in the base class is abstract, we have to override it!
        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                string courseName = this.Data[1];
                this.repository.GetAllStudentsByCourse(courseName);
            }
            else if (this.Data.Length == 3)
            {
                string courseName = this.Data[1];
                string studentName = this.Data[2];
                this.repository.GetStudentMarkInCourse(courseName, studentName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
