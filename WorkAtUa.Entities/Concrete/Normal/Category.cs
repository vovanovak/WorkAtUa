using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyCategory: Entity
    {
        
        public string Name { get; set; }

        public MyCategory()
        {
            Id = -1;
            Name = null;
        }

        public MyCategory(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
