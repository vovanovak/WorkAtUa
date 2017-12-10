using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public class CompanyRepositoryBase: RepositoryBase<MyCompanyDetail, CompanyDetail>
    {
        protected override System.Data.Entity.DbSet<CompanyDetail> GetTable()
        {
            return ctx.CompanyDetails;
        }

        protected override System.Linq.Expressions.Expression<Func<CompanyDetail, MyCompanyDetail>> GetConverter()
        {
            return c => new MyCompanyDetail
            {
                Id = c.Id,
                Name = c.Name,
                TypeOfCompany = c.TypeOfCompany,
                WebSite = c.WebSite
            };
        }

        protected override void UpdateEntry(CompanyDetail dbEntity, MyCompanyDetail entity)
        {
            dbEntity.Name = entity.Name;
            dbEntity.TypeOfCompany = entity.TypeOfCompany;
            dbEntity.WebSite = entity.WebSite;
        }

    }
}
