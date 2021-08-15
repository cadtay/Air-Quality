using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Builders
{
    public interface IQueryBuilder
    {
        IQueryBuilder Add(string key, string value);

        IQueryBuilder Add<T>(string key, T value) where T : List<string>;

        string Build();
    }
}
