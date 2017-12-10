using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyRole
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MyRole()
        {
            Id = -1;
            Name = null;
        }

        public MyRole(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
