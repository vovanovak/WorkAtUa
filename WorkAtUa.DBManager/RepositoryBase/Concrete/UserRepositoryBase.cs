using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public class UserRepositoryBase: RepositoryBase<MyUserDataGrid, User>
    {
        protected override System.Data.Entity.DbSet<User> GetTable()
        {
            return ctx.Users;
        }

        protected override System.Linq.Expressions.Expression<Func<User, MyUserDataGrid>> GetConverter()
        {
            return u => new MyUserDataGrid() { Id = u.Id, Email = u.Email, Password = u.Password, Birthday = u.Birthday,
                Name = u.Name, Surname = u.Surname, FathersName = u.FathersName, Phone = u.Phone, City = u.City.Name,
                CompanyDetails = (u.CompanyDetails == null) ? -1 : u.CompanyDetails.Value, UserDetails = (u.UserDetails == null) ? -1 : u.UserDetails.Value,
                Roles = "", Categories = "" };
        }


        protected override void UpdateEntry(User dbEntity, MyUserDataGrid entity)
        {
            dbEntity.Email = entity.Email;
            dbEntity.Password = entity.Password;
            dbEntity.Birthday = entity.Birthday;
            dbEntity.Name = entity.Name;
            dbEntity.Surname = entity.Surname;
            dbEntity.FathersName = entity.FathersName;
            dbEntity.Phone = entity.Phone;
            dbEntity.City = ctx.Cities.First(c => c.Name == entity.City);
        }

        public void AddRole(int id, string role)
        {
            User dbUser = ctx.Users.First(u => u.Id == id);
            Role dbRole = ctx.Roles.First(r => r.Name == role);

            dbUser.UserInRoles.Add(new UserInRole() { RoleId = dbRole.Id, UserId = dbUser.Id });

            ctx.SaveChanges();
        }
    }
}
