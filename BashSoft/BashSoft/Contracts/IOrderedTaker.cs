namespace BashSoft.Contracts
{
    public interface IOrderedTaker
    {
        void OrderAndTake(string courseName, string givenFilter, int? studentsToTake = null);
    }
}
