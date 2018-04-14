using BashSoft.Contracts;
using System;

namespace BashSoft
{
    public class InputReader : IReader
    {
        // Creating a private CommandInterpreter field, which we will use later
        private IInterpreter interpreter;

        // Main constructor of the class, which is setting our private field to a given parameter
        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        private const string endCommand = "quit";
        public void StartReadingCommands()
        {
            // Reading commands until we receive the end "quit" command
            while (true)
            {
                OutputWriter.WriteMessage($"{SessionData.currentPath}> ");
                string input = Console.ReadLine().Trim();
                if (input.Equals(endCommand))
                {
                    break;
                }
                if (string.IsNullOrEmpty(input))
                {
                    input = Console.ReadLine().Trim();
                    continue;
                }
                this.interpreter.InterpretCommand(input);
            }
        }
    }
}
