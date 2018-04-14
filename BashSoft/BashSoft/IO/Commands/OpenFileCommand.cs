using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using System.Diagnostics;

namespace BashSoft.IO.Commands
{
    [Alias("open")]
    public class OpenFileCommand : Command
    {
        // Main constructor of the class which receives all the parameters the base class needs
        public OpenFileCommand(string input, string[] data) : base(input, data)
        {

        }

        // As the main "Execute" method in the base class is abstract, we have to override it!
        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileName = this.Data[1];
            Process.Start(SessionData.currentPath + "\\" + fileName);
        }
    }
}
