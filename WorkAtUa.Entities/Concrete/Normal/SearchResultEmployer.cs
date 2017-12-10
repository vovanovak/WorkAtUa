using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MySearchResultEmployer
    {

        public int Id { get; set; }
        public int AgentId { get; set; }
        public int UserId { get; set; }

        public MySearchResultEmployer()
        {
            Id = -1;
            AgentId = -1;
            UserId = -1;
        }

        public MySearchResultEmployer(int id, int agentId, int uId)
        {
            Id = id;
            AgentId = agentId;
            UserId = uId;
        }
    }
}
