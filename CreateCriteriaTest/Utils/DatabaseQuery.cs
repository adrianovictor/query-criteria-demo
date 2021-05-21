using CreateCriteriaTest.Utils.Criterias;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CreateCriteriaTest.Utils
{
    public class DatabaseQuery : IDatabase, ISqlQuery
    {
        private string _query;
        private string _orderBy;
        private Criteria _criteria;

        public string Build()
        {
            var query = GenerateQuery();

            return query;
        }

        public ISqlQuery OrderBy(string field, string dir = "asc")
        {
            if (field == "")
            {
                return this;
            }

            _orderBy = $"ORDER BY {field} {dir}";

            return this;
        }

        public ISqlQuery Query(string sql)
        {
            _criteria = new Criteria();
            _query = sql;
            _orderBy = string.Empty;

            return this;
        }

        public ISqlQuery Select(string select)
        {
            _query = select;
            return this;
        }

        public ISqlQuery Where(Action<Criteria> buildCriteria)
        {
            var criteria = new Criteria();
            buildCriteria(criteria);
            _criteria = criteria;

            return this;
        }

        private string GenerateQuery()
        {
            var value = _query
                .Replace("\n", " ").Replace("\r", " ").Replace("\\r\\n", "");
            var singleLineQuery = Regex.Replace(value, @"\s", " ");
            var sqlSelect = singleLineQuery;
            var sqlWhere = GenerateWhere(string.Empty);

            if (sqlWhere != "")
            {
                sqlSelect = $"{sqlSelect} where {sqlWhere}";
            }

            var query = string.Join(" ", (new[] { sqlSelect, _orderBy }));
            query = Regex.Replace(query, @"\band\b", "AND", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            query = Regex.Replace(query, @"\basc\b", "ASC", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            query = Regex.Replace(query, @"\bdesc\b", "DESC", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            query = Regex.Replace(query, @"\bwhere\b", "WHERE", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            query = Regex.Replace(query, @"\border by\b", "ORDER BY", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            query = Regex.Replace(query, @"\bexec\b", "EXEC", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            query = Regex.Replace(query, @"\bexecute\b", "EXECUTE", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return query;
        }

        private string GenerateWhere(string original)
        {
            var whereExpressions = new StringBuilder();
            whereExpressions.Append(ExtractInnerExpression(original));

            var restrictions = _criteria.Restrictions.Select(_ => _.Generate()).ToList();
            var inlineRestrictions = string.Join(" AND ", restrictions);
            if (inlineRestrictions != "")
            {
                if (whereExpressions.Length > 0)
                {
                    whereExpressions.Append(" AND ");
                }

                whereExpressions.Append(inlineRestrictions);
            }

            return whereExpressions.Length == 0 ? string.Empty : $"({whereExpressions})";
        }

        private static string ExtractInnerExpression(string originalCriteria)
        {
            return originalCriteria.Length > 0 ? originalCriteria.Substring(1, originalCriteria.LastIndexOf(')') - 1) : "";
        }
    }
}