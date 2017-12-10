using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkAtUa.DAL;

namespace WorkAtUa.DBManager
{
    internal class DBContextSingleton
    {
        private static DBModel context = null;

        public static DBModel Context
        {
            get
            {
                if (context == null)
                    context = new DBModel();
                return context;
            }
        }
    }
}
