﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MySearchAgentEmployer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Request { get; set; }

        public MySearchAgentEmployer()
        {
            Id = -1;
            UserId = -1;
            Request = "";
        }

        public MySearchAgentEmployer(int id, int userId, string req)
        {
            Id = id;
            UserId = userId;
            Request = req;
        }
    }
}
