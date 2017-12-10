using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WorkAtUa.Entities;
using WorkAtUa.DBManager;
using WorkAtUa.Core.ServiceLocator;
using WorkAtUa.DAL;
namespace WorkAtUa.Core.WindsorInstallers
{
    public class ManagerInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDbManager>().ImplementedBy<DBManager.DbManager>().LifeStyle.Transient);
            container.Register(Component.For<IRepository<MyUserDataGrid>>().ImplementedBy<UserRepositoryBase>().LifeStyle.Transient);
            container.Register(Component.For<IRepository<MyCategory>>().ImplementedBy<CategoryRepositoryBase>().LifeStyle.Transient);
            container.Register(Component.For<IRepository<MyCity>>().ImplementedBy<CityRepositoryBase>().LifeStyle.Transient);
            container.Register(Component.For<IRepository<MyComment>>().ImplementedBy<CommentRepositoryBase>().LifeStyle.Transient);
            container.Register(Component.For<IRepository<MyCompanyDetail>>().ImplementedBy<CompanyRepositoryBase>().LifeStyle.Transient);
            container.Register(Component.For<IRepository<MyUserDetail>>().ImplementedBy<ResumeRepositoryBase>().LifeStyle.Transient);
            container.Register(Component.For<IRepository<MyVacancy>>().ImplementedBy<VacancyRepositoryBase>().LifeStyle.Transient);
        }
    }
}