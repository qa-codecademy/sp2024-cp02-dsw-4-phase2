﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.Domain.Models
{
    public class Contact : BaseEntity
    {
        public string Email { get; set; }

        public string Description { get; set; }
    }
}
