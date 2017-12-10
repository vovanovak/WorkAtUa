using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public class CategoryRepositoryBase: RepositoryBase<MyCategory, Category>
    {
        protected override System.Data.Entity.DbSet<Category> GetTable()
        {
            return ctx.Categories;
        }

        protected override System.Linq.Expressions.Expression<Func<Category, MyCategory>> GetConverter()
        {
            return u => new MyCategory()
            {
                Id = u.Id,
                Name = u.Name
            };
        }


        protected override void UpdateEntry(Category dbEntity, MyCategory entity)
        {
            dbEntity.Name = entity.Name;
        }


       
    }
}
