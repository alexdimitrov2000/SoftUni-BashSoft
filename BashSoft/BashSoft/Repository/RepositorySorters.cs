using BashSoft.Contracts;
using BashSoft.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public class RepositorySorters : IDataSorter
    {
        // Void method for ordering given number of students by a criteria which can be "Ascending" or "Descending"
        public void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison.Equals("ascending"))
            {
                this.PrintStudents(studentsWithMarks.OrderBy(x => x.Value).Take(studentsToTake).ToDictionary(k => k.Key, v => v.Value));
            }
            else if (comparison.Equals("descending"))
            {
                this.PrintStudents(studentsWithMarks.OrderByDescending(x => x.Value).Take(studentsToTake).ToDictionary(k => k.Key, v => v.Value));
            }
            else
            {
                throw new InvalidComparisonQueryException();
            }
        }

        // Private void method, for printing the sorted dictionary of students, which is used only in the current class
        private void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (KeyValuePair<string, double> keyValuePair in studentsSorted)
            {
                OutputWriter.DisplayStudent(keyValuePair);
            }
        }

        // These methods below have been used but are not necessary anymore
        /*
        private static void OrderAndTake(Dictionary<string, List<int>> wantedData, int studentsToTake, //Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparisonFunc)
        {
            Dictionary<string, List<int>> studentsSorted = GetSortedStudents(wantedData, studentsToTake, //comparisonFunc);
        
            foreach (KeyValuePair<string, List<int>> student in studentsSorted)
            {
                OutputWriter.DisplayStudent(student);
            }
        }
        
        private static int CompareInOrder(KeyValuePair<string, List<int>> firstValue,
            KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (int i in firstValue.Value)
            {
                totalOfFirstMarks += i;
            }
        
            int totalOfSecondMarks = 0;
            foreach (int i in secondValue.Value)
            {
                totalOfSecondMarks += i;
            }
        
            return totalOfSecondMarks.CompareTo(totalOfFirstMarks);
        }
        
        private static int CompareDescendingOrder(KeyValuePair<string, List<int>> firstValue,
            KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (int i in firstValue.Value)
            {
                totalOfFirstMarks += i;
            }
        
            int totalOfSecondMarks = 0;
            foreach (int i in secondValue.Value)
            {
                totalOfSecondMarks += i;
            }
        
            return totalOfFirstMarks.CompareTo(totalOfSecondMarks);
        }
        
        private static Dictionary<string, List<int>> GetSortedStudents(Dictionary<string, List<int>> //studentsWanted, int takeCount,
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> Comparison)
        {
            int valuesTaken = 0;
            Dictionary<string, List<int>> studentsSorted = new Dictionary<string, List<int>>();
            KeyValuePair<string, List<int>> nextInOrder = new KeyValuePair<string, List<int>>();
            bool isSorted = false;
        
            while (valuesTaken < takeCount)
            {
                isSorted = true;
        
                foreach (KeyValuePair<string, List<int>> studentsWithScore in studentsWanted)
                {
                    if (!string.IsNullOrEmpty(nextInOrder.Key))
                    {
                        int comparisonResults = Comparison(studentsWithScore, nextInOrder);
        
                        if (comparisonResults >= 0 && !studentsSorted.ContainsKey(studentsWithScore.Key))
                        {
                            nextInOrder = studentsWithScore;
                            isSorted = false;
                        }
                    }
                    else
                    {
                        if (!studentsSorted.ContainsKey(studentsWithScore.Key))
                        {
                            nextInOrder = studentsWithScore;
                            isSorted = false;
                        }
                    }
                }
        
                if (!isSorted)
                {
                    studentsSorted.Add(nextInOrder.Key, nextInOrder.Value);
                    valuesTaken++;
                    nextInOrder = new KeyValuePair<string, List<int>>();
                }
            }
        
            return studentsSorted;
        }  */
    }
}
