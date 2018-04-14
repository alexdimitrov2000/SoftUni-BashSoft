using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("cmp")]
    public class CompareFilesCommand : Command
    {
        [Inject]
        private string[] data;

        [Inject]
        private IContentComparer judge;

        // Main constructor of the class which receives all the parameters the base class needs
        public CompareFilesCommand(string input, string[] data) : base(input, data)
        {

        }

        // As the main "Execute" method in the base class is abstract, we have to override it!
        public override void Execute()
        {
            if (this.data.Length == 3)
            {
                string firstPath = this.data[1];
                string secondPath = this.data[2];

                this.judge.CompareContent(firstPath, secondPath);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
