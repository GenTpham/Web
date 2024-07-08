using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demo.Data;
using Demo.Models;

namespace Demo.Controllers
{
    public class RevenueReportsController : Controller
    {
        private readonly DemoContext _context;

        public RevenueReportsController(DemoContext context)
        {
            _context = context;
        }

        // GET: RevenueReports
        public async Task<IActionResult> Index()
        {
              return _context.RevenueReport != null ? 
                          View(await _context.RevenueReport.ToListAsync()) :
                          Problem("Entity set 'DemoContext.RevenueReport'  is null.");
        }

        // GET: RevenueReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RevenueReport == null)
            {
                return NotFound();
            }

            var revenueReport = await _context.RevenueReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revenueReport == null)
            {
                return NotFound();
            }

            return View(revenueReport);
        }

        // GET: RevenueReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RevenueReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,TotalRevenue,TotalExpense,NetProfit")] RevenueReport revenueReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(revenueReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(revenueReport);
        }

        // GET: RevenueReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RevenueReport == null)
            {
                return NotFound();
            }

            var revenueReport = await _context.RevenueReport.FindAsync(id);
            if (revenueReport == null)
            {
                return NotFound();
            }
            return View(revenueReport);
        }

        // POST: RevenueReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,TotalRevenue,TotalExpense,NetProfit")] RevenueReport revenueReport)
        {
            if (id != revenueReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(revenueReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevenueReportExists(revenueReport.Id))
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
            return View(revenueReport);
        }

        // GET: RevenueReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RevenueReport == null)
            {
                return NotFound();
            }

            var revenueReport = await _context.RevenueReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (revenueReport == null)
            {
                return NotFound();
            }

            return View(revenueReport);
        }

        // POST: RevenueReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RevenueReport == null)
            {
                return Problem("Entity set 'DemoContext.RevenueReport'  is null.");
            }
            var revenueReport = await _context.RevenueReport.FindAsync(id);
            if (revenueReport != null)
            {
                _context.RevenueReport.Remove(revenueReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RevenueReportExists(int id)
        {
          return (_context.RevenueReport?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
