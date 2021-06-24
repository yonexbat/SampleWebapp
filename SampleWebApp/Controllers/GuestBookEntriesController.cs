using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleWebApp.Data;
using SampleWebApp.Models.Entities;

namespace SampleWebApp.Controllers
{
    public class GuestBookEntriesController : Controller
    {
        private readonly SampleWebAppContext _context;

        public GuestBookEntriesController(SampleWebAppContext context)
        {
            _context = context;
        }

        // GET: GuestBookEntries
        public async Task<IActionResult> Index()
        {
            return View(await _context.GuestBookEntry.ToListAsync());
        }

        // GET: GuestBookEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBookEntry = await _context.GuestBookEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestBookEntry == null)
            {
                return NotFound();
            }

            return View(guestBookEntry);
        }

        // GET: GuestBookEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GuestBookEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text")] GuestBookEntry guestBookEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guestBookEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guestBookEntry);
        }

        // GET: GuestBookEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBookEntry = await _context.GuestBookEntry.FindAsync(id);
            if (guestBookEntry == null)
            {
                return NotFound();
            }
            return View(guestBookEntry);
        }

        // POST: GuestBookEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text")] GuestBookEntry guestBookEntry)
        {
            if (id != guestBookEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guestBookEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuestBookEntryExists(guestBookEntry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(guestBookEntry);
        }

        // GET: GuestBookEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestBookEntry = await _context.GuestBookEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guestBookEntry == null)
            {
                return NotFound();
            }

            return View(guestBookEntry);
        }

        // POST: GuestBookEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guestBookEntry = await _context.GuestBookEntry.FindAsync(id);
            _context.GuestBookEntry.Remove(guestBookEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuestBookEntryExists(int id)
        {
            return _context.GuestBookEntry.Any(e => e.Id == id);
        }
    }
}
