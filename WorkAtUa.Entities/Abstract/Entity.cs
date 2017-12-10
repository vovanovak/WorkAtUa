﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAtUa.Entities
{
    public class Entity
    {
        public int Id { get; set; }

        protected Entity()
        {
            Id = -1;
        }

        protected Entity(int id)
        {
            Id = id;
        }

        public bool IsNew()
        {
            return Id == -1;
        }
    }
}
