using CreateCriteriaTest.Utils.Criterias;
using System;

namespace CreateCriteriaTest.Utils
{
    public interface ISqlQuery
    {
        ISqlQuery Select(string select);

        ISqlQuery Where(Action<Criteria> buildCriteria);

        ISqlQuery OrderBy(string field, string dir = "asc");

        string Build();
    }
}