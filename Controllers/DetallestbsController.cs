using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using carnetutelvt.Models;

namespace carnetutelvt.Controllers
{
    public class DetallestbsController : Controller
    {
        private readonly rgutelvtContext _context;

        public DetallestbsController(rgutelvtContext context)
        {
            _context = context;
        }

        // GET: Detallestbs
        public async Task<IActionResult> Index()
        {
            var rgutelvtContext = _context.Detallestbs.Include(d => d.IduserNavigation);
            return View(await rgutelvtContext.ToListAsync());
        }

        // GET: Detallestbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Detallestbs == null)
            {
                return NotFound();
            }

            var detallestb = await _context.Detallestbs
                .Include(d => d.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallestb == null)
            {
                return NotFound();
            }

            return View(detallestb);
        }

        // GET: Detallestbs/Create
        public IActionResult Create()
        {
            ViewData["Iduser"] = new SelectList(_context.Usertbs, "Id", "Id");
            return View();
        }

        // POST: Detallestbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname,Surnames,Specialty,Faculty,Ci,Imgcarnet,Iduser,Dateupdate,Datecreate")] Detallestb detallestb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallestb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iduser"] = new SelectList(_context.Usertbs, "Id", "Id", detallestb.Iduser);
            return View(detallestb);
        }

        // GET: Detallestbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Detallestbs == null)
            {
                return NotFound();
            }

            var detallestb = await _context.Detallestbs.FindAsync(id);
            if (detallestb == null)
            {
                return NotFound();
            }
            ViewData["Iduser"] = new SelectList(_context.Usertbs, "Id", "Id", detallestb.Iduser);
            return View(detallestb);
        }

        // POST: Detallestbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fullname,Surnames,Specialty,Faculty,Ci,Imgcarnet,Iduser,Dateupdate,Datecreate")] Detallestb detallestb)
        {
            if (id != detallestb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallestb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallestbExists(detallestb.Id))
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
            ViewData["Iduser"] = new SelectList(_context.Usertbs, "Id", "Id", detallestb.Iduser);
            return View(detallestb);
        }

        // GET: Detallestbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Detallestbs == null)
            {
                return NotFound();
            }

            var detallestb = await _context.Detallestbs
                .Include(d => d.IduserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallestb == null)
            {
                return NotFound();
            }

            return View(detallestb);
        }

        // POST: Detallestbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Detallestbs == null)
            {
                return Problem("Entity set 'rgutelvtContext.Detallestbs'  is null.");
            }
            var detallestb = await _context.Detallestbs.FindAsync(id);
            if (detallestb != null)
            {
                _context.Detallestbs.Remove(detallestb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallestbExists(int id)
        {
          return _context.Detallestbs.Any(e => e.Id == id);
        }
    }
}
