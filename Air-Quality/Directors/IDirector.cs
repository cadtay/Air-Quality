using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Air_Quality.Directors
{
    public interface IDirector<in TParameters, out TResult> where TParameters : IDirectorParameters<TResult> 
    {
        TResult Execute(TParameters parameters);
    }

    public interface IDirector<out TResult>
    {
        TResult ExecuteAsync();
    }
}
