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
    public class BookingsController : Controller
    {
        private readonly DemoContext _context;

        public BookingsController(DemoContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var demoContext = _context.Booking.Include(b => b.Promotion).Include(b => b.User).Include(b => b.room);
            return View(await demoContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name");
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,CheckInDate,CheckOutDate,Adult,Child,Request,PromotionCode,RoomId,UserId,IsCheckedOut,ActualCheckOutDate,PromotionId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Payment), new { id = booking.Id });
            }
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name", booking.PromotionId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name", booking.UserId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name", booking.PromotionId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name", booking.UserId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,CheckInDate,CheckOutDate,Adult,Child,Request,PromotionCode,RoomId,UserId,IsCheckedOut,ActualCheckOutDate,PromotionId")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["PromotionId"] = new SelectList(_context.Promotion, "Id", "Name", booking.PromotionId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Name", booking.UserId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Promotion)
                .Include(b => b.User)
                .Include(b => b.room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Booking == null)
            {
                return Problem("Entity set 'DemoContext.Booking'  is null.");
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
          return (_context.Booking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // GET: Bookings/Checkout/5
        public async Task<IActionResult> Checkout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.room)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            if (booking.IsCheckedOut)
            {
                return BadRequest("This booking has already been checked out.");
            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCheckout(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.IsCheckedOut = true;
            booking.ActualCheckOutDate = DateTime.Now;

            _context.Update(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyBookings", "Users");
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Cancel/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            if (_context.Booking == null)
            {
                return Problem("Entity set 'DemoContext.Booking' is null.");
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("MyBookings", "Users");

        }

        // GET: Bookings/Payment/5
        public async Task<IActionResult> Payment(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.room)
                .Include(b => b.Promotion)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            int totalDays = (booking.CheckOutDate - booking.CheckInDate).Days;
            // Tính toán giá tiền sau khuyến mãi
            decimal originalPrice = booking.room.Price * totalDays;
            decimal discountedPrice = originalPrice;
            if (booking.Promotion != null)
            {
                discountedPrice = originalPrice * (100 - booking.Promotion.DiscountPercentage) / 100;
            }
            ViewBag.TotalDays = totalDays;
            ViewBag.OriginalPrice = originalPrice;
            ViewBag.DiscountedPrice = discountedPrice;

            return View(booking);
        }

        // POST: Bookings/Payment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(int id, string paymentMethod)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Xử lý thanh toán ở đây (tích hợp với cổng thanh toán thực tế)
            // Đây chỉ là mô phỏng
            booking.IsCheckedOut = true;
            booking.ActualCheckOutDate = DateTime.Now;

            _context.Update(booking);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ThankYou));
        }
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
