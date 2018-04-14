using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
    [Alias("mkdir")]
    public class MakeDirectoryCommand : Command
    {
        [Inject]
        private string[] data;

        [Inject]
        private IDirectoryManager inputOutputManager;

        // Main constructor of the class which receives all the parameters the base class needs
        public MakeDirectoryCommand(string input, string[] data) : base(input, data)
        {
        }

        // As the main "Execute" method in the base class is abstract, we have to override it!
        public override void Execute()
        {
            if (this.data.Length == 2)
            {
                string folderName = this.data[1];
                this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}