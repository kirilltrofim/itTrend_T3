using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Univer.Data;
using Univer.Models;
using Univer.Service.Courses;

namespace Univer.Controllers
{
    public class CoursesController : Controller
    {
        private ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courseList = _courseService.List();

            return View(await Task.Run(() => courseList));
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetById((int) id);

            if (course == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => course));
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["Groups"] = new SelectList(_courseService.GroupList(), "Id", "Title");
            ViewData["Instructors"] = new SelectList(_courseService.InstructorList(), "Id", "FullName");
            ViewData["Students"] = new SelectList(_courseService.StudentList(), "Id", "FullName");

            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course, List<Int32> list, List<Int32> list1, List<Int32> list2)
        {
            if (ModelState.IsValid)
            {
                _courseService.Create(course, list, list1, list2);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }

            ViewData["Groups"] = new SelectList(_courseService.GroupList(), "Id", "Title");
            ViewData["Instructors"] = new SelectList(_courseService.InstructorList(), "Id", "FullName");
            ViewData["Students"] = new SelectList(_courseService.StudentList(), "Id", "FullName");

            return View(await Task.Run(() => course));
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetById((int) id);

            if (course == null)
            {
                return NotFound();
            }

            ViewData["Groups"] = new SelectList(_courseService.GroupList(), "Id", "Title");
            ViewData["Instructors"] = new SelectList(_courseService.InstructorList(), "Id", "FullName");
            ViewData["Students"] = new SelectList(_courseService.StudentList(), "Id", "FullName");

            return View(await Task.Run(() => course));
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course, List<Int32> list, List<Int32> list1, List<Int32> list2)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _courseService.Update(id, course, list, list1, list2);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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

            ViewData["Groups"] = new SelectList(_courseService.GroupList(), "Id", "Title");
            ViewData["Instructors"] = new SelectList(_courseService.InstructorList(), "Id", "FullName");
            ViewData["Students"] = new SelectList(_courseService.StudentList(), "Id", "FullName");

            return View(await Task.Run(() => course));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courseService.GetById((int) id);

            if (course == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => course));
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _courseService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool CourseExists(int id)
        {
            return _courseService.CourseExists(id);
        }
    }
}
