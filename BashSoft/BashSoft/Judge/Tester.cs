using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.IO;

namespace BashSoft
{
    public class Tester : IContentComparer
    {
        // Method for comparing the content of two paths
        public void CompareContent(string userOutputPath, string expectedOutputPath)
        {

            // Trying to excecute the main logic of the method
            // If it detects an exception we throw a new InvalidPathException
            try
            {
                OutputWriter.WriteMessageOnNewLine($"Reading files...");
                string mismatchPath = GetMismatchPath(expectedOutputPath);

                string[] actualOutputLines = File.ReadAllLines(userOutputPath);
                string[] expectedOutputLines = File.ReadAllLines(expectedOutputPath);

                bool hasMismatch;
                string[] mismatches = GetLinesWithPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);

                this.PrintOutput(mismatches, hasMismatch, mismatchPath);
                OutputWriter.WriteMessageOnNewLine("Files read!");
            }
            catch (IOException)
            {
                throw new InvalidPathException();
            }
        }

        private void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
        {
            if (hasMismatch)
            {
                foreach (string mismatch in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(mismatch);
                }
                //try
                //{
                File.WriteAllLines(mismatchPath, mismatches);
                //}
                //catch (DirectoryNotFoundException)
                //{
                //    OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                //}
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine($"Files are identical. There are no mismatches.");
            }
        }

        private string[] GetLinesWithPossibleMismatches(string[] actualOutputLines, string[] expectedOutputLines, out bool hasMismatch)
        {
            hasMismatch = false;
            string output = string.Empty;

            string[] mismatches = new string[actualOutputLines.Length];
            OutputWriter.WriteMessageOnNewLine($"Comparing files...");

            int minOutputLine = actualOutputLines.Length;
            if (actualOutputLines.Length != expectedOutputLines.Length)
            {
                hasMismatch = true;
                minOutputLine = Math.Min(actualOutputLines.Length, expectedOutputLines.Length);
                OutputWriter.DisplayException(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
            }

            for (int index = 0; index < minOutputLine; index++)
            {
                string actualLine = actualOutputLines[index];
                string expectedLine = expectedOutputLines[index];

                if (!actualLine.Equals(expectedLine))
                {
                    output = $"Mismatch at line {index} -- expected: \"{expectedLine}\", actual: \"{actualLine}\"";
                    output += Environment.NewLine;
                    hasMismatch = true;
                }
                else
                {
                    output = actualLine;
                    output += Environment.NewLine;
                }
                mismatches[index] = output;
            }
            return mismatches;
        }

        // Method for getting the MismatchPath from the expected ouptut path
        private string GetMismatchPath(string expectedOutputPath)
        {
            int indexOf = expectedOutputPath.LastIndexOf('\\');
            string directoryPath = expectedOutputPath.Substring(0, indexOf);

            // Add the "Mismatches" file name with the extension ".txt" to the directoryPath
            string finalPath = directoryPath + @"\Mismatches.txt";
            return finalPath;
        }
    }
}
