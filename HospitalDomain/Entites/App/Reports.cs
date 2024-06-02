﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.App
{
    public class Reports : BaseEntity
    {
        public Reports()
        {
            Medicines = new HashSet<Medicines>();
        }

        public string GuestId { get; set; }
        public string EmployeeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Medicines> Medicines { get; set; }
    }
}