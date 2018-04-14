using System;
using System.Collections.Generic;

namespace BashSoft
{
    public static class OutputWriter
    {
        public static void WriteMessage(string message)
        {
            Console.Write(message);
        }

        public static void WriteMessageOnNewLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        public static void DisplayException(string message)
        {
            // Saving the current console color in a variable, which we will need later
            ConsoleColor currentConsoleColor = Console.ForegroundColor;
            // Set a new foreground color on the console
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            // Back to the first and default foreground color of console
            Console.ForegroundColor = currentConsoleColor;
        }

        public static void DisplayStudent(KeyValuePair<string, double> student)
        {
            OutputWriter.WriteMessageOnNewLine($"{student.Key} - {student.Value}");
        }
    }
}
