using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class MyCompanyDetail: Entity
    {
        
        public string Name { get; set; }
        public int TypeOfCompany { get; set; }
        public string WebSite { get; set; }

        public MyCompanyDetail()
        {
            Id = -1;
            Name = null;
            TypeOfCompany = -1;
            WebSite = null;
        }

        public MyCompanyDetail(int id, string name, int typeOfCompany, string webSite)
        {
            Id = id;
            Name = name;
            TypeOfCompany = typeOfCompany;
            WebSite = webSite;
        }
    }
}
