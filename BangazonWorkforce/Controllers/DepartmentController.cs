using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BangazonWorkforce.Models;
using BangazonWorkforce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace BangazonWorkforce.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _config;
        public DepartmentController(IConfiguration config)
        {
            DepartmentRepository.SetConfig(config);
        }
        public ActionResult Index()
        {
            List<Department> departments = DepartmentRepository.GetDepartments();
            return View(departments);

        }

        // GET: Departments/Details/XXX
        public ActionResult Details(int id)
        {
            Department department = DepartmentRepository.GetOneDepartment(id);
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            Department department = new Department();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department department)
        {
            try
            {
                DepartmentRepository.CreateDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Departments/Delete/XXX
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Department department = DepartmentRepository.GetOneDepartment(id);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection formCollection)
        {
            try
            {
                DepartmentRepository.DeleteDepartment(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}