using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.Models
{
    public class NewManager
    {
        [Key]
        public int ManagerId { get; set; }
        public string? Mname { get; set; }

        public List<NewEmployee>? employees { get; set; }
    }
}
