using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.Collections.Generic;

namespace BashSoft
{
    public class RepositoryFilters : IDataFilter
    {
        // Void method for filtering a given number of students by a wanted filter which can be "excellent", "average" or "poor"
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int studentsToTake)
        {
            if (wantedFilter == "excellent")
            {
                this.FilterAndTake(studentsWithMarks, x => x >= 5, studentsToTake);
            }
            else if (wantedFilter == "average")
            {
                this.FilterAndTake(studentsWithMarks, x => x >= 3.5 && x < 5, studentsToTake);
            }
            else if (wantedFilter == "poor")
            {
                this.FilterAndTake(studentsWithMarks, x => x < 3.5, studentsToTake);
            }
            else
            {
                throw new InvalidStudentFilterException();
            }
        }

        // Private void method doing some work only for the current class
        private void FilterAndTake(Dictionary<string, double> studentsWithMarks, Predicate<double> givenFilter,
            int studentsToTake)
        {
            int counterForPrinted = 0;
            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }
                
                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.DisplayStudent(new KeyValuePair<string, double>(studentMark.Key, studentMark.Value));
                    counterForPrinted++;
                }
            }
        }

        // Innecessary private methods that have been needed before
        /*private static bool ExcellentFilter(double mark)
        {
            return mark >= 5.0;
        }
        
        private static bool AverageFilter(double mark)
        {
            return mark < 5.0 && mark >= 3.5;
        }
        
        private static bool PoorFilter(double mark)
        {
            return mark < 3.5;
        }

        private static double Average(List<int> scoresOnTask)
        {
            int totalScore = 0;
            foreach (int score in scoresOnTask)
            {
                totalScore += score;
            }
        
            double percentageOfAll = totalScore / (scoresOnTask.Count * 100.0);
            double mark = percentageOfAll * 4 + 2;
        
            return mark;
        } */
    }
}
