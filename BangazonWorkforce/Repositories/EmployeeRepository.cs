using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace BangazonWorkforce.Controllers
{
    public class EmployeeController : Controller
    {

        public EmployeeController(IConfiguration config)
        {
            EmployeeRepository.SetConfig(config);
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> allEmployees = EmployeeRepository.GetAllEmployees();
            return View(allEmployees);
        }

        // GET: Exercise/Details/5
        public ActionResult Details(int id)
        {
            EmployeeViewModel vm = new EmployeeViewModel(id);
            return View(vm);
        }

        // GET: Exercise/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercise/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Exercise/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Exercise/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Exercise/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Exercise/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}