using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalDomain.Entites.App
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string EmployeeId { get; set; }

        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public Session Session { get; set; }

        public int HistoryId { get; set; }

        [ForeignKey("HistoryId")]
        public History History { get; set; }
    }
}