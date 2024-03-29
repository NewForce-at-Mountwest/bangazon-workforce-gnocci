﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangazonWorkforce.Models;
using BangazonWorkforce.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BangazonWorkforce.Controllers
{
    public class TrainingProgramsController : Controller
    {

        public TrainingProgramsController(IConfiguration config)
        {

            TrainingProgramRepository.SetConfig(config);

        }





        // GET: TrainingPrograms
        public ActionResult Index()
        {
            List<TrainingProgram> trainingPrograms = TrainingProgramRepository.GetTrainingPrograms();
                return View(trainingPrograms);
        }

        // GET: TrainingPrograms/Details/5
        public ActionResult Details(int id)
        {
            TrainingProgram trainingProgram = TrainingProgramRepository.GetOneTrainingProgramWithAttendingEmployees(id);
            return View(trainingProgram);
        }

        // GET: TrainingPrograms/Create
        public ActionResult Create()
        {
            TrainingProgram trainingProgram = new TrainingProgram();
            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrainingProgram trainingProgram)
        {
            try
            {
                TrainingProgramRepository.CreateTrainingProgram(trainingProgram);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainingPrograms/Edit/5
        public ActionResult Edit(int id)
        {
            TrainingProgram trainingProgram = TrainingProgramRepository.GetOneTrainingProgram(id);
            return View(trainingProgram);
        }

        // POST: TrainingPrograms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TrainingProgram trainingProgram)
        {
            try
            {

                TrainingProgramRepository.UpdateTrainingProgram(id, trainingProgram);   
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(trainingProgram);
            }
        }

        // GET: TrainingPrograms/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrainingPrograms/Delete/5
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