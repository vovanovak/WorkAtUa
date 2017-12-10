using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkAtUa.Tools.Logger;
using Castle.MicroKernel.Registration;

namespace WorkAtUa.Core.WindsorInstallers
{
    public class ToolsInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<ILogger>().ImplementedBy<Log4netLogger>().LifeStyle.Singleton);
        }
    }
}