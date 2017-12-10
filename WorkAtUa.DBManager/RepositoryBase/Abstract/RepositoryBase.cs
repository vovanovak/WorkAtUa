using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public abstract class RepositoryBase<T, DbT> : IRepository<T>
             where T : Entity where DbT : DbEntity, new() 
    {
        protected DBModel ctx = DBContextSingleton.Context;

        public List<T> GetAll()
        {
            return GetTable().Select(GetConverter()).ToList();
        }

        public Entity GetEntityById(int id)
        {
            return GetTable().Where(t => t.Id == id).Select(GetConverter()).First();
        }

        public bool Save(T entity)
        {
            DbT e;
            if (entity.Id == -1)
            {
                e = new DbT();
            }
            else
            {
                e = GetTable().Where(x => x.Id == entity.Id).First();
                if (e == null)
                    return false;
            }

            UpdateEntry(e, entity);

            if (entity.IsNew())
            {
                GetTable().Add(e);
            }

            ctx.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var dbEntity = GetTable().Where(x => x.Id == id).SingleOrDefault();
            if (dbEntity == null)
                return false;

            GetTable().Remove(dbEntity);
            ctx.SaveChanges();
            return true;
        }

        public bool Delete(T entity)
        {
            return Delete(entity.Id);
        }

        protected abstract DbSet<DbT> GetTable();
        protected abstract Expression<Func<DbT, T>> GetConverter();
        protected abstract void UpdateEntry(DbT dbEntity, T entity);
        
    }
}
