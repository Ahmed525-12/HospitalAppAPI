using System.ComponentModel.DataAnnotations.Schema;

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