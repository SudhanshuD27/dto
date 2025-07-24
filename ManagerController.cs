using EmployeeMVC.Models;
using EmployeeMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class ManagerController : Controller
    {
        IManagerService mgr;

        public ManagerController(IManagerService mgr)
        {
            this.mgr = mgr;
        }

        public IActionResult Index()
        {
            var data = mgr.DisplayManagers();
            return View(data);
        }

        public IActionResult AddManager()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddManager(Manager m)
        {
            mgr.AddManager(m);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteManager(int id)
        {
            mgr.DeleteManager(id);
            return RedirectToAction("Index");
        }

        public IActionResult EditManager(int id)
        {
            var data = mgr.FindManagerById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditManager(Manager m)
        {
            mgr.UpdateManager(m);
            return RedirectToAction("Index");
        }
    }
}

