using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Univer.Data;
using Univer.Models;
using Univer.Service.Instructors;
using Microsoft.AspNetCore.Http;

namespace Univer.Controllers
{
    public class InstructorsController : Controller
    {
        private IInstructorService _instructorService;

        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            var list = _instructorService.List();
            return View(await Task.Run(() => list));
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = _instructorService.GetById((int) id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => instructor));
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            ViewData["Group"] = new SelectList(_instructorService.GroupList(null), "Id", "Title");
            ViewData["Courses"] = new SelectList(_instructorService.CourseList(), "Id", "Title");

            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor,IFormFile uploadFile, int? name, List<Int32> list)
        {
            if (ModelState.IsValid)
            {
                _instructorService.Create(instructor, uploadFile, name, list);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }

            ViewData["Group"] = new SelectList(_instructorService.GroupList(null), "Id", "Title");
            ViewData["Courses"] = new SelectList(_instructorService.CourseList(), "Id", "Title");

            return View(await Task.Run(() => instructor));
        }

        // GET: Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = _instructorService.GetById((int) id);

            if (instructor == null)
            {
                return NotFound();
            }

            ViewData["Group"] = new SelectList(_instructorService.GroupList(id), "Id", "Title", instructor.Group);
            ViewData["Courses"] = new SelectList(_instructorService.CourseList(), "Id", "Title");

            return View(await Task.Run(() => instructor));
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor, IFormFile uploadFile, int? name, List<Int32> list)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _instructorService.Update(id, instructor, uploadFile, name, list);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }

            ViewData["Group"] = new SelectList(_instructorService.GroupList(id), "Id", "Title");
            ViewData["Courses"] = new SelectList(_instructorService.CourseList(), "Id", "Title");

            return View(await Task.Run(() => instructor));
        }

        // GET: Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = _instructorService.GetById((int) id);

            if (instructor == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => instructor));
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _instructorService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool InstructorExists(int id)
        {
            return _instructorService.InstructorExists(id);
        }
    }
}
