using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BangazonWorkforce.Models;
using BangazonWorkforce.Repositories;
using BangazonWorkforce.Models.ViewModels;

namespace BangazonWorkforce.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IConfiguration config)
        {
            EmployeeRepository.SetConfig(config);
            DepartmentRepository.SetConfig(config);
        }

        // GET: Employees
        public ActionResult Index()
        {
            List<Employee> allEmployees = EmployeeRepository.GetEmployees();
            return View(allEmployees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = EmployeeRepository.GetOneEmployee(id);
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            CreateEmployeeViewModel employee = new CreateEmployeeViewModel();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEmployeeViewModel model)
        {
            try
            {
                EmployeeRepository.CreateEmployee(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }


        }



        // GET: Employees/Edit/3
        public ActionResult Edit(int id)
        {
            EditEmployeeViewModel employee = new EditEmployeeViewModel(id);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditEmployeeViewModel viewModel)
        {
            try
            {
                EmployeeRepository.UpdateEmployee(id, viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(viewModel);
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