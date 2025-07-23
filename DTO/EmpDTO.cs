namespace EmployeeMVC.DTO
{
    public class EmpDTO
    {
        public int eid { get; set; }
        public string ename { get; set; }
        public string email { get; set; }
        public string esalary { get; set; }

        public int ManagerId { get; set; }
        public string? ManagerName { get; set; }
    }
}
