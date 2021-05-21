using System;
using System.Collections.Generic;

namespace CreateCriteriaTest.Utils.Restrictions
{
    public class EqualsRestriction : RestrictionCriteria
    {
        public string Field { get; }
        public object Value { get; }

        public EqualsRestriction(string field, object value)
        {
            if (string.IsNullOrEmpty(field)) throw new ArgumentNullException("field");
            if (value == null) throw new ArgumentNullException("value");

            Field = field;
            Value = value;
        }

        public override string Generate()
        {
            return $"{Field} = {Value}";
        }

        protected override IEnumerable<object> LoadParameters()
        {
            return new[] { Value };
        }
    }
}