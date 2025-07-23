using EmployeeMVC.Data;
using EmployeeMVC.DTO;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers
{
    public class EmployeeHandlerController : Controller
    {
        ApplicationDbContext db;
        public EmployeeHandlerController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.managers = new SelectList(db.newManagers.ToList(), "ManagerId", "Mname");
            return View();
        }

        public IActionResult AddEmployees(EmpDTO e)
        {
            var em = new NewEmployee
            {
                email = e.email,
                ename = e.ename,
                esalary = e.esalary,
                ManagerId = e.ManagerId
            };
            db.newEmp.Add(em);
            db.SaveChanges();
            return Json("");
        }

        public IActionResult FetchEmps()
        {
            var d = db.newEmp.Include(x => x.mans)
                    .Select(e => new EmpDTO
                    {
                        eid = e.eid,
                        ename = e.ename,
                        email = e.email,
                        esalary = e.esalary,
                        ManagerId = e.ManagerId,
                        ManagerName = e.mans != null ? e.mans.Mname : "No"
                    }).ToList();
            return Json(d);
        }

        public IActionResult GetEmpById(int id)
        {
            var e = db.newEmp.Find(id);
            return Json(e);
        }

        public IActionResult UpdateEmployee(NewEmployee e)
        {
            db.newEmp.Update(e);
            db.SaveChanges();
            return Json("");
        }
    }
}
