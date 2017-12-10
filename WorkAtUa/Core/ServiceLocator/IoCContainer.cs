using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkAtUa.Tools.ServiceLocator;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace WorkAtUa.Core.ServiceLocator
{

    public static class IoCContainer
    {
        private static IServiceLocator _instance = null;
        private static WindsorContainer _windsorContainer = null;

        public static void Initialize()
        {
            _windsorContainer = new WindsorContainer();
            _windsorContainer.Install(FromAssembly.This());
            _instance = new WindsorServiceLocator(_windsorContainer);
        }

        public static void Dispose()
        {
            if (_windsorContainer != null)
            {
                _windsorContainer.Dispose();
            }
        }

        public static IServiceLocator ServiceLocator
        {
            get
            {
                return _instance;
            }
        }
    }
}