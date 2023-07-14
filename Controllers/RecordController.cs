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
    public class RecordController : Controller
    {
        private readonly MyWayContext _context;

        public RecordController(MyWayContext context)
        {
            _context = context;
        }

        // GET: Record
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var myWayContext = _context.Records.Include(r => r.Event).Include(r => r.Grade).Include(r => r.Skill);
            return View(await myWayContext.ToListAsync());
        }

        // GET: Record/Details/5
        [HttpGet]
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(r => r.Event)
                .Include(r => r.Grade)
                .Include(r => r.Skill)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // GET: Record/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeId");
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillId");
            return View();
        }

        // POST: Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("RecordId,UserId,EventId,SkillId,GradeId")] Record record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", record.EventId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeId", record.GradeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillId", record.SkillId);
            return View(record);
        }

        // GET: Record/Edit/5
        [HttpGet]
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record = await _context.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", record.EventId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeId", record.GradeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillId", record.SkillId);
            return View(record);
        }

        // POST: Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(long id, [Bind("RecordId,UserId,EventId,SkillId,GradeId")] Record record)
        {
            if (id != record.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(record.RecordId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId", record.EventId);
            ViewData["GradeId"] = new SelectList(_context.Grades, "GradeId", "GradeId", record.GradeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillId", record.SkillId);
            return View(record);
        }

        // GET: Record/Delete/5
        [HttpGet]
        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Records == null)
            {
                return NotFound();
            }

            var record = await _context.Records
                .Include(r => r.Event)
                .Include(r => r.Grade)
                .Include(r => r.Skill)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // [HttpPost]
        // [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Records == null)
            {
                return Problem("Entity set 'MyWayContext.Records'  is null.");
            }
            var record = await _context.Records.FindAsync(id);
            if (record != null)
            {
                _context.Records.Remove(record);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(long id)
        {
            return (_context.Records?.Any(e => e.RecordId == id)).GetValueOrDefault();
        }
    }
}
