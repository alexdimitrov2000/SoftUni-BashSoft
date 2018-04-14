using BashSoft.Contracts;

namespace BashSoft
{
    class Launcher
    {
        public static void Main()
        {
            #region comments
            // These comments below are some tests that I used to use from C# Advanced course

            /* P1 Tests
            IOManager.TraverseDirectory(@"D:\Downloads");

            P2 Tests
            StudentsRepository.InitializeData();
            StudentsRepository.GetAllStudentsByCourse("Unity");
            StudentsRepository.GetStudentMarkInCourse("Unity", "Ivan");

            P3 Tests
            Tester.CompareContent(@".\BashSoft-Resources\test1.txt", @".\BashSoft-Resources\test2.txt");
            Tester.CompareContent(@".\BashSoft-Resources\test1.txt", @".\BashSoft-Resources\test3.txt");

            P4 Tests
            IOManager.CreateDirectoryInCurrentFolder("Pesho");
            IOManager.TraverseDirectory(5);
            IOManager.CreateDirectoryInCurrentFolder("Pesho");
            IOManager.ChangeCurrentDirectoryRelative("Pesho");
            IOManager.ChangeCurrentDirectoryAbsolute("Pesho");
            IOManager.ChangeCurrentDirectoryAbsolute(@"C:\Windows");
            IOManager.TraverseDirectory(20);
            Tester.CompareContent("actual", "expecter");
            IOManager.CreateDirectoryInCurrentFolder("*2");

            InputReader.StartReadingCommands(); */ 
            #endregion

            IContentComparer tester = new Tester();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentsRepository(new RepositoryFilters(), new RepositorySorters());

            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }
}
