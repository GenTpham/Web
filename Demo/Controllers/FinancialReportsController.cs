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
    public class FinancialReportsController : Controller
    {
        private readonly DemoContext _context;

        public FinancialReportsController(DemoContext context)
        {
            _context = context;
        }

        // GET: FinancialReports
        public async Task<IActionResult> Index()
        {
              return _context.FinancialReport != null ? 
                          View(await _context.FinancialReport.ToListAsync()) :
                          Problem("Entity set 'DemoContext.FinancialReport'  is null.");
        }

        // GET: FinancialReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FinancialReport == null)
            {
                return NotFound();
            }

            var financialReport = await _context.FinancialReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financialReport == null)
            {
                return NotFound();
            }

            return View(financialReport);
        }

        // GET: FinancialReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FinancialReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportDate,TotalRevenue,TotalExpenses,NumberOfBookings,Notes")] FinancialReport financialReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(financialReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(financialReport);
        }

        // GET: FinancialReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FinancialReport == null)
            {
                return NotFound();
            }

            var financialReport = await _context.FinancialReport.FindAsync(id);
            if (financialReport == null)
            {
                return NotFound();
            }
            return View(financialReport);
        }

        // POST: FinancialReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReportDate,TotalRevenue,TotalExpenses,NumberOfBookings,Notes")] FinancialReport financialReport)
        {
            if (id != financialReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialReportExists(financialReport.Id))
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
            return View(financialReport);
        }

        // GET: FinancialReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FinancialReport == null)
            {
                return NotFound();
            }

            var financialReport = await _context.FinancialReport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (financialReport == null)
            {
                return NotFound();
            }

            return View(financialReport);
        }

        // POST: FinancialReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FinancialReport == null)
            {
                return Problem("Entity set 'DemoContext.FinancialReport'  is null.");
            }
            var financialReport = await _context.FinancialReport.FindAsync(id);
            if (financialReport != null)
            {
                _context.FinancialReport.Remove(financialReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialReportExists(int id)
        {
          return (_context.FinancialReport?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
