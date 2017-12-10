using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;

namespace WorkAtUa.Tools.ServiceLocator
{
    public class WindsorServiceLocator : IServiceLocator
    {
        #region Fields

        private readonly WindsorContainer _windsorContainer;

        #endregion

        #region Constructors

        public WindsorServiceLocator(WindsorContainer windsorContainer)
        {
            this._windsorContainer = windsorContainer;
        }

        #endregion

        #region IServiceLocator Members

        public object Resolve(Type serviceType)
        {
            return this._windsorContainer.Resolve(serviceType);
        }

        public object Resolve(Type serviceType, string key)
        {
            return this._windsorContainer.Resolve(key, serviceType);
        }

        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            return this._windsorContainer.ResolveAll<object>(serviceType);
        }

        public TService Resolve<TService>()
        {
            return this._windsorContainer.Resolve<TService>();
        }

        public TService Resolve<TService>(string key)
        {
            return this._windsorContainer.Resolve<TService>(key);
        }

        public IEnumerable<TService> ResolveAll<TService>()
        {
            return this._windsorContainer.ResolveAll<TService>();
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            return this._windsorContainer.Resolve(serviceType);
        }

        #endregion
    }
}
