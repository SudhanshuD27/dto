using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeMVC.Models
{
    public class NewEmployee
    {
        [Key]
        public int eid { get; set; }
        public string ename { get; set; }
        public string email { get; set; }
        public string esalary { get; set; }


        [ForeignKey("mans")]
        public int ManagerId { get; set; }
        public NewManager? mans { get; set; }
    }
}
