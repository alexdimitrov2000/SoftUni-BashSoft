using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.Collections.Generic;

namespace BashSoft.Models
{
    public class SoftUniCourse : ICourse
    {
        // Class fields
        private string name;
        private Dictionary<string, IStudent> studentsByName;

        // Class constant integers
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        // Main constructor of the class
        public SoftUniCourse(string name)
        {
            this.name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }
        
        // Public properties...
        public string Name
        {
            get { return this.name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(this.name), ExceptionMessages.NullOrEmptyValue);
                }

                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName
        {
            get { return this.studentsByName; }
        }

        // Method for enrolling a current student in certain course
        public void EnrollStudent(IStudent student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName, this.Name);
            }

            this.studentsByName.Add(student.UserName, student);
        }

        public int CompareTo(ICourse other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString() => this.Name;
    }
}
