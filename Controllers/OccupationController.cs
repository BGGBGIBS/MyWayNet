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
    public class OccupationController : Controller
    {
        private readonly MyWayContext _context;

        public OccupationController(MyWayContext context)
        {
            _context = context;
        }

        // GET: Occupation
        public async Task<IActionResult> Index()
        {
              return _context.Occupations != null ? 
                          View(await _context.Occupations.ToListAsync()) :
                          Problem("Entity set 'MyWayContext.Occupations'  is null.");
        }

        // GET: Occupation/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Occupations == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupations
                .FirstOrDefaultAsync(m => m.OccupationId == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // GET: Occupation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Occupation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OccupationId,OccupationName,OccupationDescription")] Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(occupation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(occupation);
        }

        // GET: Occupation/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Occupations == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupations.FindAsync(id);
            if (occupation == null)
            {
                return NotFound();
            }
            return View(occupation);
        }

        // POST: Occupation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("OccupationId,OccupationName,OccupationDescription")] Occupation occupation)
        {
            if (id != occupation.OccupationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occupation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccupationExists(occupation.OccupationId))
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
            return View(occupation);
        }

        // GET: Occupation/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Occupations == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupations
                .FirstOrDefaultAsync(m => m.OccupationId == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // POST: Occupation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Occupations == null)
            {
                return Problem("Entity set 'MyWayContext.Occupations'  is null.");
            }
            var occupation = await _context.Occupations.FindAsync(id);
            if (occupation != null)
            {
                _context.Occupations.Remove(occupation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationExists(long id)
        {
          return (_context.Occupations?.Any(e => e.OccupationId == id)).GetValueOrDefault();
        }
    }
}
