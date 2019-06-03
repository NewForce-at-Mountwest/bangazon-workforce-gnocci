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
    public class ComputerController : Controller
    {
        private readonly IConfiguration _config;

        public ComputerController(IConfiguration config)
        {
            ComputerRepository.SetConfig(config);
        }

        public ActionResult Index()
        {
            List<Computer> computers = ComputerRepository.GetComputers();
            return View(computers);

        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            Computer computer = ComputerRepository.GetOneComputer(id);
            return View(computer);

        }

        // GET: Students/Create
        public ActionResult Create()
        {
            Computer computer = new Computer();
            return View(computer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Computer computer)
        {
            try
            {
                ComputerRepository.CreateComputer(computer);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Computer computer = ComputerRepository.GetOneComputer(id);
            return View(computer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection formCollection)
        {
            try
            {

                ComputerRepository.DeleteComputer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        //------- EDIT BUT I DIDN'T NEED TO DO EDIT FOR MY TICKET =)) ----------
        //public ActionResult Edit(int id)
        //{
        //    Computer computer = ComputerRepository.GetOneComputer(id);
        //    return View(computer);
        //}


        //// POST: Students/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Computer computer)
        //{
        //    try
        //    {
        //        ComputerRepository.UpdateComputer(id, computer);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception)
        //    {
        //        return View(computer);
        //    }
        //}

    }
}