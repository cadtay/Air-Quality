using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Quality.Builders
{
    public class QueryBuilder : IQueryBuilder
    {
        private StringBuilder _queryStringBuilder = new StringBuilder();
        public IQueryBuilder Add(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
                return this;

            _queryStringBuilder.Append(_queryStringBuilder.Length == 0 ? "?" : "&");

            _queryStringBuilder.Append(key);
            _queryStringBuilder.Append("=");
            _queryStringBuilder.Append(Uri.EscapeDataString(value));

            return this;
        }

        public IQueryBuilder Add<T>(string key, T value) where T : List<string>
        {
            if (value == null)
                return this;

            _queryStringBuilder.Append(_queryStringBuilder.Length == 0 ? "?" : "&");


            for (int i = 0; i < value.Count; i++)
            {
                _queryStringBuilder.Append(key);
                _queryStringBuilder.Append("=");
                _queryStringBuilder.Append(Uri.EscapeDataString(value[i]));

                if (i != value.Count - 1)
                    _queryStringBuilder.Append("&");
            }
           
            return this;
        }

        public string Build()
        {
            return _queryStringBuilder.ToString();
        }
    }
}
