using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface IRequester
    {
        void GetStudentMarkInCourse(string courseName, string studentName);
        void GetAllStudentsByCourse(string courseName);

        ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp);

        ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp);
    }
}
