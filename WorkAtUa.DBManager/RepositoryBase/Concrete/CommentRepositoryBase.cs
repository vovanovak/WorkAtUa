using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;
using WorkAtUa.Entities;

namespace WorkAtUa.DBManager
{
    public class CommentRepositoryBase: RepositoryBase<MyComment, Comment>
    {
        protected override System.Data.Entity.DbSet<Comment> GetTable()
        {
            return ctx.Comments;
        }

        protected override System.Linq.Expressions.Expression<Func<Comment, MyComment>> GetConverter()
        {
            return c => new MyComment {
                Id = c.Id, 
                UserId = c.UserId,
                VacancyId = c.VacancyId,
                Date = c.Date,
                Content = c.Content};
        } 

        protected override void UpdateEntry(Comment dbEntity, MyComment entity)
        {
            dbEntity.UserId = entity.UserId;
            dbEntity.VacancyId = entity.VacancyId;
            dbEntity.Date = entity.Date;
            dbEntity.Content = entity.Content;
        }

       
    }
}
