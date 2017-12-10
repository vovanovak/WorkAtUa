using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public class ResumeRepositoryBase: RepositoryBase<MyUserDetail, UserDetail>
    {
        protected override System.Data.Entity.DbSet<UserDetail> GetTable()
        {
            return ctx.UserDetails;
        }

        protected override System.Linq.Expressions.Expression<Func<UserDetail, MyUserDetail>> GetConverter()
        {
            return r => new MyUserDetail()
            {
                Id = r.Id,
                Header = r.Header,
                Salary = r.Salary,
                Description = r.Description,
                Education = (r.EducationId == null) ? -1 : r.EducationId.Value,
                JobExperience = (r.JobExperienceId == null) ? -1 : r.JobExperienceId.Value,
            };
        }

        protected override void UpdateEntry(UserDetail dbEntity, MyUserDetail entity)
        {
            dbEntity.Header = entity.Header;
            dbEntity.Salary = entity.Salary;
            dbEntity.Description = entity.Description;

            if (entity.Education == -1)
                dbEntity.EducationId = null;
            else
                dbEntity.EducationId = entity.Education;

            if (entity.JobExperience == -1)
                dbEntity.JobExperienceId = null;
            else
                dbEntity.JobExperienceId = entity.JobExperience;
        }

       
    }
}
