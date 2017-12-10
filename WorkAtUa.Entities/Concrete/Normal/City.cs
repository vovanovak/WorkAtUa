using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyCity: Entity
    {
       
        public string Name { get; set; }

        public MyCity()
        {
            Id = -1;
            Name = null;
        }

        public MyCity(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
