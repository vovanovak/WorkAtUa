using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Tools.ServiceLocator
{
    public interface IServiceLocator : IServiceProvider
    {
        object Resolve(Type serviceType);
        object Resolve(Type serviceType, string key);
        IEnumerable<object> ResolveAll(Type serviceType);
        TService Resolve<TService>();
        TService Resolve<TService>(string key);
        IEnumerable<TService> ResolveAll<TService>();
    }
}
