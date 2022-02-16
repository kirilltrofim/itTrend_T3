using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Univer.Data;
using Univer.Models;
using Univer.Service.Students;

namespace Univer.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var studentList = _studentService.List(); 

            return View(await Task.Run(() => studentList));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.GetById((int) id);

            if (student == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => student));
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Course"] = new SelectList(_studentService.CourseList(), "Id", "Title");
            ViewData["Group"] = new SelectList(_studentService.GroupList(), "Id", "Title");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student, IFormFile uploadFile)
        {
            if (ModelState.IsValid)
            {
                _studentService.Create(student, uploadFile);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }
            ViewData["Course"] = new SelectList(_studentService.CourseList(), "Id", "Title", student.CourseId);
            ViewData["Group"] = new SelectList(_studentService.GroupList(), "Id", "Title", student.GroupId);
            return View(await Task.Run(() => student));
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.GetById((int) id);

            if (student == null)
            {
                return NotFound();
            }

            ViewData["Course"] = new SelectList(_studentService.CourseList(), "Id", "Title", student.CourseId);
            ViewData["Group"] = new SelectList(_studentService.GroupList(), "Id", "Title", student.GroupId);

            return View(await Task.Run(() => student));
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student, IFormFile uploadFile)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _studentService.Update(id, student, uploadFile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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

            ViewData["Course"] = new SelectList(_studentService.CourseList(), "Id", "Title", student.CourseId);
            ViewData["Group"] = new SelectList(_studentService.GroupList(), "Id", "Title", student.GroupId);

            return View(await Task.Run(() => student));
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentService.GetById((int) id);

            if (student == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => student));
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _studentService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool StudentExists(int id)
        {
            return _studentService.StudentExists(id);
        }
    }
}
