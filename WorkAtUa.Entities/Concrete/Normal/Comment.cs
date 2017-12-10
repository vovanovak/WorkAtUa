using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyComment:Entity
    {
       
        public int UserId { get; set; }
        public int VacancyId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public MyComment()
        {
            Id = -1;
            UserId = -1;
            VacancyId = -1;
            Date = new DateTime();
            Content = null;
        }

        public MyComment(int id, int userId, int vacancyId, DateTime date, string content)
        {
            Id = id;
            UserId = userId;
            VacancyId = vacancyId;
            Date = date;
            Content = content;
        }
    }
}
