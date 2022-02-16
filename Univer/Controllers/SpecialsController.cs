using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Univer.Data;
using Univer.Models;
using Univer.Service.Specials;

namespace Univer.Controllers
{
    public class SpecialsController : Controller
    {
        private ISpecialService _specialService;

        public SpecialsController(ISpecialService specialService)
        {
            _specialService = specialService;
        }

        // GET: Specials
        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => _specialService.List()));
        }

        // GET: Specials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var special = _specialService.GetById((int) id);

            if (special == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => special));
        }

        // GET: Specials/Create
        public IActionResult Create()
        {
            ViewData["Groups"] = new SelectList(_specialService.GroupList(), "Id", "Title");

            return View();
        }

        // POST: Specials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Special special, List<Int32> list)
        {
            if (ModelState.IsValid)
            {
                _specialService.Create(special, list);
                return RedirectToAction(await Task.Run(() => nameof(Index)));
            }

            ViewData["Groups"] = new SelectList(_specialService.GroupList(), "Id", "Title");

            return View(await Task.Run(() => special));
        }

        // GET: Specials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var special = _specialService.GetById((int) id);

            if (special == null)
            {
                return NotFound();
            }

            ViewData["Groups"] = new SelectList(_specialService.GroupList(), "Id", "Title");

            return View(await Task.Run(() => special));
        }

        // POST: Specials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Special special, List<Int32> list)
        {
            if (id != special.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _specialService.Update(id, special, list);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialExists(special.Id))
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

            ViewData["Groups"] = new SelectList(_specialService.GroupList(), "Id", "Title");

            return View(await Task.Run(() => special));
        }

        // GET: Specials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var special = _specialService.GetById((int) id);

            if (special == null)
            {
                return NotFound();
            }

            return View(await Task.Run(() => special));
        }

        // POST: Specials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _specialService.Delete(id);
            return RedirectToAction(await Task.Run(() => nameof(Index)));
        }

        private bool SpecialExists(int id)
        {
            return _specialService.SpecialExists(id);
        }
    }
}
