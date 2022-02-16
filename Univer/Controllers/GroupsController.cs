using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Univer.Data;
using Univer.Models;
using Univer.Service.Groups;

namespace Univer.Controllers
{
    public class GroupsController : Controller
    {
        private IGroupService _groupService;

        public GroupsController(IGroupService groupservice)
        {
            _groupService = groupservice;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var list = _groupService.List();
            return View(await Task.Run(() => list));
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = _groupService.GetById((int) id);

            if (@group == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => @group));
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            ViewData["Curator"] = new SelectList(_groupService.CuratorList(null), "Id", "FullName");
            ViewData["Course"] = new SelectList(_groupService.CourseList(), "Id", "Title");
            ViewData["Special"] = new SelectList(_groupService.SpecialList(), "Id", "Title");
            ViewData["Students"] = new SelectList(_groupService.StudentList(null), "Id", "FullName");

            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group @group, List<Int32> list)
        {
            if (ModelState.IsValid)
            {
                _groupService.Create(group, list);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }

            ViewData["Course"] = new SelectList(_groupService.CourseList(), "Id", "Title", @group.CourseId);
            ViewData["Curator"] = new SelectList(_groupService.CuratorList(null), "Id", "FullName", @group.CuratorId);
            ViewData["Special"] = new SelectList(_groupService.SpecialList(), "Id", "Title", @group.SpecialId);
            ViewData["Students"] = new SelectList(_groupService.StudentList(null), "Id", "FullName");

            return View(await Task.Run(() => @group));
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = _groupService.GetById((int) id);

            if (@group == null)
            {
                return NotFound();
            }
            ViewData["Course"] = new SelectList(_groupService.CourseList(), "Id", "Title", @group.CourseId);
            ViewData["Curator"] = new SelectList(_groupService.CuratorList((int) id), "Id", "FullName", @group.CuratorId);
            ViewData["Special"] = new SelectList(_groupService.SpecialList(), "Id", "Title", @group.SpecialId);
            ViewData["Students"] = new SelectList(_groupService.StudentList(id), "Id", "FullName");

            return View(await Task.Run(() => @group));
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Group @group, List<Int32> list)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _groupService.Update(id, @group, list);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
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
            ViewData["Course"] = new SelectList(_groupService.CourseList(), "Id", "Title", @group.CourseId);
            ViewData["Curator"] = new SelectList(_groupService.CuratorList((int) id), "Id", "FullName", @group.CuratorId);
            ViewData["Special"] = new SelectList(_groupService.SpecialList(), "Id", "Title", @group.SpecialId);
            ViewData["Students"] = new SelectList(_groupService.StudentList(id), "Id", "FullName");

            return View(await Task.Run(() => @group));
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = _groupService.GetById((int) id);

            if (@group == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => @group));
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _groupService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool GroupExists(int id)
        {
            return _groupService.GroupExists(id);
        }
    }
}
