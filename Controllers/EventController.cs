using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWayNet.Models;

namespace MyWayNet.Controllers
{
    [ApiController]
[Route("[controller]")]
    public class EventController : Controller
    {
        private readonly MyWayContext _context;

        public EventController(MyWayContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var myWayContext = _context.Events.Include(e => e.Institution).Include(e => e.Occupation);
            return View(await myWayContext.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventM = await _context.Events
                .Include(e => e.Institution)
                .Include(e => e.Occupation)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventM == null)
            {
                return NotFound();
            }

            return View(eventM);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "InstitutionId", "InstitutionId");
            ViewData["OccupationId"] = new SelectList(_context.Occupations, "OccupationId", "OccupationId");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventBegin,EventEnd,OccupationId,InstitutionId,EventType")] Event eventM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "InstitutionId", "InstitutionId", eventM.InstitutionId);
            ViewData["OccupationId"] = new SelectList(_context.Occupations, "OccupationId", "OccupationId", eventM.OccupationId);
            return View(eventM);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventM = await _context.Events.FindAsync(id);
            if (eventM == null)
            {
                return NotFound();
            }
            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "InstitutionId", "InstitutionId", eventM.InstitutionId);
            ViewData["OccupationId"] = new SelectList(_context.Occupations, "OccupationId", "OccupationId", eventM.OccupationId);
            return View(eventM);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EventId,EventBegin,EventEnd,OccupationId,InstitutionId,EventType")] Event eventM)
        {
            if (id != eventM.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventM.EventId))
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
            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "InstitutionId", "InstitutionId", eventM.InstitutionId);
            ViewData["OccupationId"] = new SelectList(_context.Occupations, "OccupationId", "OccupationId", eventM.OccupationId);
            return View(eventM);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventM = await _context.Events
                .Include(e => e.Institution)
                .Include(e => e.Occupation)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventM == null)
            {
                return NotFound();
            }

            return View(eventM);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'MyWayContext.Events'  is null.");
            }
            var eventM = await _context.Events.FindAsync(id);
            if (eventM != null)
            {
                _context.Events.Remove(eventM);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(long id)
        {
          return (_context.Events?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
