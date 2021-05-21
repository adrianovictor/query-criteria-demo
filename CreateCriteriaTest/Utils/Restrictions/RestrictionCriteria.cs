using System.Collections.Generic;
using System.Linq;

namespace CreateCriteriaTest.Utils.Restrictions
{
    public abstract class RestrictionCriteria : IRestrictionCriteria
    {
        public abstract string Generate();

        public IDictionary<string, object> GetParameters()
        {
            var parameters = LoadParameters().ToList();
            return parameters.Select((p, i) => new { i, p }).ToDictionary(_ => $"{_.i}", _ => _.p);
        }

        protected abstract IEnumerable<object> LoadParameters();
    }
}