using BashSoft.Contracts;
using BashSoft.DataStructures;
using BashSoft.Exceptions;
using BashSoft.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BashSoft
{
    public class StudentsRepository : IDatabase
    {
        // Private fields which we need for this class
        private bool isDataInitialized = false;
        private RepositoryFilters filter;
        private RepositorySorters sorter;
        private Dictionary<string, ICourse> courses;
        private Dictionary<string, IStudent> students;

        // Constructor for the current class which recieves a RepositorySorter and a RepositoryFilter as parameters
        public StudentsRepository(RepositoryFilters filter, RepositorySorters sorter)
        {
            this.filter = filter;
            this.sorter = sorter;
        }

        // Main method in this class, used for reading data and accepting a fileName string as a parameter
        private void ReadData(string fileName)
        {
            string path = SessionData.currentPath + @"\" + fileName;

            if (File.Exists(path))
            {
                // Regex pattern used for detecting all the valid input lines
                string pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex regex = new Regex(pattern);

                string[] allInputLines = File.ReadAllLines(path);


                foreach (string line in allInputLines)
                {
                    if (!string.IsNullOrEmpty(line) && regex.IsMatch(line))
                    {
                        Match currentMatch = regex.Match(line);

                        string courseName = currentMatch.Groups[1].Value;
                        string username = currentMatch.Groups[2].Value;
                        string studentsStr = currentMatch.Groups[3].Value;
                        try
                        {
                            int[] scores = studentsStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse)
                                .ToArray();

                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                                continue;
                            }

                            if (scores.Length > SoftUniCourse.MaxScoreOnExamTask)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.students.ContainsKey(username))
                            {
                                this.students.Add(username, new SoftUniStudent(username));
                            }
                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new SoftUniCourse(courseName));
                            }

                            // Taking the currently needed course and student
                            ICourse course = this.courses[courseName];
                            IStudent student = this.students[username];

                            // Using the previously created methods in the Student and Course classes
                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, scores);

                            course.EnrollStudent(student);
                        }
                        catch (FormatException fex)
                        {
                            OutputWriter.DisplayException(fex.Message + $"at line : {line}");
                        }
                    }
                }
            }
            else
            {
                throw new InvalidPathException();
            }

            isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine($"Data read!");
        }

        // Public method for ordering a number of students by a given filter
        public void OrderAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }
                
                Dictionary<string, double> marks = 
                    this.courses[courseName].StudentsByName.ToDictionary(k => k.Key, v => v.Value.MarksByCourseName[courseName]);

                this.sorter.OrderAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        // Public method for filtering a number of students by a given filter
        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (this.IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks =
                    this.courses[courseName].StudentsByName.ToDictionary(k => k.Key, v => v.Value.MarksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                throw new DataAlreadyInitializedException();
            }

            this.students = new Dictionary<string, IStudent>();
            this.courses = new Dictionary<string, ICourse>();
            OutputWriter.WriteMessageOnNewLine("Reading data...");
            ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!isDataInitialized)
            {
                throw new DataNotInitializedException();
            }
            
            this.courses = null;
            this.students = null;
            this.isDataInitialized = false;
        }

        // Private method that returns a boolean and checks if a query is possible for a certain course
        private bool IsQueryForCoursePossible(string courseName)
        {   
            if (this.isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
                throw new ArgumentException(ExceptionMessages.InexistingCourseInDataBase);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
        }

        // Private method that returns a boolean and checks if a query is possible for a certain student
        private bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (this.IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentsByName.ContainsKey(studentUserName))
            {
                return true;
            }

            throw new ArgumentException(ExceptionMessages.InexistingStudentInDataBase);
        }
        
        // Method for calculating the score of a certain student in a course and printing the student himself
        public void GetStudentMarkInCourse(string courseName, string studentName)
        {
            if (IsQueryForStudentPossible(courseName, studentName))
            {
                OutputWriter.DisplayStudent
                    (new KeyValuePair<string, double>(studentName, this.courses[courseName].StudentsByName[studentName].MarksByCourseName[courseName]));
            }
        }

        public void GetAllStudentsByCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}");

                foreach (var studentMarksEntry in this.courses[courseName].StudentsByName)
                {
                    this.GetStudentMarkInCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        public ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp)
        {
            SimpleSortedList<ICourse> sortedCourses = new SimpleSortedList<ICourse>(cmp);
            sortedCourses.AddAll(this.courses.Values);

            return sortedCourses;
        }

        public ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp)
        {
            SimpleSortedList<IStudent> sortedStudents = new SimpleSortedList<IStudent>(cmp);
            sortedStudents.AddAll(this.students.Values);

            return sortedStudents;
        }
    }
}
