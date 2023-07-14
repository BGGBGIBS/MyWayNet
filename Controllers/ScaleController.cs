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
    public class ScaleController : Controller
    {
        private readonly MyWayContext _context;

        public ScaleController(MyWayContext context)
        {
            _context = context;
        }

        // GET: Scale
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return _context.Scales != null ?
                        View(await _context.Scales.ToListAsync()) :
                        Problem("Entity set 'MyWayContext.Scales'  is null.");
        }

        // GET: Scale/Details/5
        [HttpGet]
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Scales == null)
            {
                return NotFound();
            }

            var scale = await _context.Scales
                .FirstOrDefaultAsync(m => m.ScaleId == id);
            if (scale == null)
            {
                return NotFound();
            }

            return View(scale);
        }

        // GET: Scale/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Scale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("ScaleId,ScaleMin,ScaleMax")] Scale scale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scale);
        }

        // GET: Scale/Edit/5
        [HttpGet]
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Scales == null)
            {
                return NotFound();
            }

            var scale = await _context.Scales.FindAsync(id);
            if (scale == null)
            {
                return NotFound();
            }
            return View(scale);
        }

        // POST: Scale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("ScaleId,ScaleMin,ScaleMax")] Scale scale)
        {
            if (id != scale.ScaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScaleExists(scale.ScaleId))
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
            return View(scale);
        }

        // GET: Scale/Delete/5
        [HttpGet]
        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Scales == null)
            {
                return NotFound();
            }

            var scale = await _context.Scales
                .FirstOrDefaultAsync(m => m.ScaleId == id);
            if (scale == null)
            {
                return NotFound();
            }

            return View(scale);
        }

        // POST: Scale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [HttpPost]
        // [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Scales == null)
            {
                return Problem("Entity set 'MyWayContext.Scales'  is null.");
            }
            var scale = await _context.Scales.FindAsync(id);
            if (scale != null)
            {
                _context.Scales.Remove(scale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScaleExists(int id)
        {
            return (_context.Scales?.Any(e => e.ScaleId == id)).GetValueOrDefault();
        }
    }
}
