using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MySearchResult
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int VacancyId { get; set; }

        public MySearchResult()
        {
            Id = -1;
            AgentId = -1;
            VacancyId = -1;
        }

        public MySearchResult(int id, int agentId, int vacancyId)
        {
            Id = id;
            AgentId = agentId;
            VacancyId = vacancyId;
        }
    }
}
