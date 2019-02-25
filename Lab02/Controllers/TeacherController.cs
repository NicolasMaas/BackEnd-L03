using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab02.Models;
using Lab02.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherRepo teacherRepo;
        public TeacherController(ITeacherRepo teacherRepo)
        {
            this.teacherRepo = teacherRepo;
        }

        // GET: Teacher
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public async Task<ActionResult> IndexAsync()
        {
            IEnumerable<Teacher> model = await teacherRepo.GetAllAsync();
            return View(model);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Teacher/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}