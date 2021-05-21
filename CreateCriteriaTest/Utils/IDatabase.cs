namespace CreateCriteriaTest.Utils
{
    public interface IDatabase
    {
        ISqlQuery Query(string sql);
    }
}