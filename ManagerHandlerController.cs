using EmployeeMVC.Data;
using EmployeeMVC.DTO;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class ManagerHandlerController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;

        public ManagerHandlerController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FetchManagers()
        {
            var data = db.newManagers.Select(m => new ManagerDTO
            {
                ManagerId = m.ManagerId,
                Mname = m.Mname
            }).ToList();

            return Json(data);
        }

        public IActionResult AddManager(ManagerDTO m)
        {
            var man = new NewManager
            {
                Mname = m.Mname
            };

            db.newManagers.Add(man);
            db.SaveChanges();
            return Json("");
        }

        public IActionResult GetManagerById(int id)
        {
            var m = db.newManagers.Find(id);
            return Json(m);
        }

        public IActionResult UpdateManager(NewManager m)
        {
            db.newManagers.Update(m);
            db.SaveChanges();
            return Json("");
        }

        public IActionResult DeleteManager(int id)
        {
            try
            {
                var m = db.newManagers.Find(id);
                if (m == null)
                    return Json("Not Found");

                bool isAssignedToEmp = db.newEmp.Any(e => e.ManagerId == id);
                if (isAssignedToEmp)
                {
                    throw new Exception("This manager is assigned to one or more employees and cannot be deleted.");
                }

                db.newManagers.Remove(m);
                db.SaveChanges();
                return Json("Deleted");
            }
            catch (Exception ex)
            {
                LogErrorToFile(ex.Message);
                return StatusCode(500, "Cannot delete. Error logged.");
            }
        }

        private void LogErrorToFile(string message)
        {
            string logPath = Path.Combine(env.WebRootPath, "logs");
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            string fullPath = Path.Combine(logPath, "error_log.txt");
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
                writer.WriteLine("---------------------------");
            }
        }
    }
}