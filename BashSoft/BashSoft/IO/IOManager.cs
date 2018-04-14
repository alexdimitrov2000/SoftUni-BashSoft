using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;

namespace BashSoft
{
    public class IOManager : IDirectoryManager
    {
        public void TraverseDirectory(int depth)
        {
            OutputWriter.WriteEmptyLine();
            int initialIndentation = SessionData.currentPath.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(SessionData.currentPath);

            while (subFolders.Count > 0)
            {
                string currentPath = subFolders.Dequeue();

                int indentation = currentPath.Split('\\').Length - initialIndentation;

                if (depth - indentation < 0)
                {
                    break;
                }
                OutputWriter.WriteMessageOnNewLine($"{new string('-', indentation)}{currentPath}");

                try
                {
                    string[] files = Directory.GetFiles(currentPath);

                    foreach (string file in files)
                    {
                        int indexOfLastSlash = file.IndexOf(@"\");
                        string fileName = file.Substring(indexOfLastSlash);
                        OutputWriter.WriteMessageOnNewLine($"{new string('-', indexOfLastSlash)}{fileName}");
                    }

                    string[] directories = Directory.GetDirectories(currentPath);

                    foreach (string directory in directories)
                    {
                        subFolders.Enqueue(directory);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.UnauthorizedAccessExceptionMessage);
                }
            }
        }

        public void CreateDirectoryInCurrentFolder(string name)
        {
            string path = GetCurrentDirectoryPath() + @"\" + name;
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (ArgumentException)
            {
                throw new InvalidFileNameException();
            }
        }

        private string GetCurrentDirectoryPath()
        {
            return SessionData.currentPath;
        }

        public void ChangeCurrentDirectoryRelative(string relativePath)
        {
            if (relativePath == "..")
            {
                try
                {
                    string currentPath = SessionData.currentPath;
                    int indexOfLastSlash = currentPath.LastIndexOf(@"\");
                    string newPath = currentPath.Substring(0, indexOfLastSlash);
                    SessionData.currentPath = newPath;
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new InvalidPathException();
                }
            }
            else
            {
                string currentPath = SessionData.currentPath;
                currentPath += @"\" + relativePath;
                ChangeCurrentDirectoryAbsolute(currentPath);
            }
        }

        public void ChangeCurrentDirectoryAbsolute(string absolutePath)
        {
            if (!Directory.Exists(absolutePath))
            {
                throw new InvalidPathException();
                //OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                //return;
            }

            SessionData.currentPath = absolutePath;
        }
    }
}
