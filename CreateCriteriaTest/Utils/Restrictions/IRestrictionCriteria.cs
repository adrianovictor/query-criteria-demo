using System.Collections.Generic;

namespace CreateCriteriaTest.Utils.Restrictions
{
    public interface IRestrictionCriteria
    {
        string Generate();

        IDictionary<string, object> GetParameters();
    }
}