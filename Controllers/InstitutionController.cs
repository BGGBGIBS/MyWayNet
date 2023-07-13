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
    [Route("api/[controller]")]
    public class InstitutionController : Controller
    {
        private readonly MyWayContext _context;

        public InstitutionController(MyWayContext context)
        {
            _context = context;
        }

        // GET: Institution
        [HttpGet]
        // [Route("Index")]
        public async Task<IActionResult> Index()
        {
              return _context.Institutions != null ? 
                          View(await _context.Institutions.ToListAsync()) :
                          Problem("Entity set 'MyWayContext.Institutions'  is null.");
        }

        // GET: Institution/Details/5
        [HttpGet]
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Institutions == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions
                .FirstOrDefaultAsync(m => m.InstitutionId == id);
            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
        }

        // GET: Institution/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Institution/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("InstitutionId,InstitutionName,InstitutionType,InstitutionAddress")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institution);
        }

        // GET: Institution/Edit/5
        [HttpGet]
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Institutions == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions.FindAsync(id);
            if (institution == null)
            {
                return NotFound();
            }
            return View(institution);
        }

        // POST: Institution/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, [Bind("InstitutionId,InstitutionName,InstitutionType,InstitutionAddress")] Institution institution)
        {
            if (id != institution.InstitutionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitutionExists(institution.InstitutionId))
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
            return View(institution);
        }

        // GET: Institution/Delete/5
        [HttpGet]
        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Institutions == null)
            {
                return NotFound();
            }

            var institution = await _context.Institutions
                .FirstOrDefaultAsync(m => m.InstitutionId == id);
            if (institution == null)
            {
                return NotFound();
            }

            return View(institution);
        }

        // POST: Institution/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [HttpPost]
        // [Route("Delete/{id}")]

        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Institutions == null)
            {
                return Problem("Entity set 'MyWayContext.Institutions'  is null.");
            }
            var institution = await _context.Institutions.FindAsync(id);
            if (institution != null)
            {
                _context.Institutions.Remove(institution);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitutionExists(long id)
        {
          return (_context.Institutions?.Any(e => e.InstitutionId == id)).GetValueOrDefault();
        }
    }
}
