using CreateCriteriaTest.Utils.Restrictions;
using System.Collections.Generic;
using System.Linq;

namespace CreateCriteriaTest.Utils.Criterias
{
    public class Criteria
    {
        private readonly IList<IRestrictionCriteria> _restrictions = new List<IRestrictionCriteria>();
        public IEnumerable<IRestrictionCriteria> Restrictions => _restrictions.Skip(0);

        public Criteria Add(IRestrictionCriteria restrictions)
        {
            _restrictions.Add(restrictions);

            return this;
        }

        /// <summary>
        /// Equal to
        /// </summary>
        /// <param name="field">Field</param>
        /// <param name="value">Value</param>
        /// <returns>Criteria</returns>
        public Criteria Eq(string field, string value)
        {
            _restrictions.Add(new EqualsRestriction(field, value));
            return this;
        }

        public Criteria Null(string field)
        {
            return this;
        }

        public Criteria NotNull(string field)
        {
            return this;
        }
    }
}