using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.Entites.App
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public Medicines Medicines { get; set; }

        [ForeignKey("Medicines")]
        public int MedicinesId { get; set; }
    }
}