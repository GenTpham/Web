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

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,CheckInDate,CheckOutDate,Request,RoomId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();

                // Lưu ID của đặt phòng vào TempData
                TempData["BookingId"] = booking.Id;

                // Chuyển hướng sang trang thanh toán
                return RedirectToAction(nameof(Payment));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", booking.RoomId);
            return View(booking);
        }

        public async Task<IActionResult> Payment()
        {
            if (!TempData.ContainsKey("BookingId"))
            {
                return RedirectToAction("Index", "Home");
            }

            int bookingId = (int)TempData["BookingId"];
            var bookingInfo = await _context.Booking
                .Include(b => b.room)  // Đảm bảo rằng thông tin phòng được include
                .FirstOrDefaultAsync(b => b.Id == bookingId);

            if (bookingInfo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // Lấy danh sách các loại phòng và giá
            var rooms = await _context.Room.Select(r => new
            {
                r.Id,
                r.RoomType,
                r.Price
            }).ToListAsync();

            ViewData["Rooms"] = new SelectList(rooms, "Id", "RoomType", bookingInfo.RoomId);
            ViewData["RoomPrices"] = rooms.ToDictionary(r => r.Id, r => r.Price);

            return View(bookingInfo);
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPayment()
        {
            // Xử lý thanh toán

            // Sau khi thanh toán thành công, chuyển hướng đến trang cảm ơn
            return RedirectToAction("ThankYou", "Bookings");
        }

        public IActionResult ThankYou()
        {
            return View();
        }


        public async Task<IActionResult> Index()
        {
            var demoContext = _context.Booking.Include(b => b.room);
            return View(await demoContext.ToListAsync());
        }
        public async Task<IActionResult> Admin_Index()
        {
            var demoContext = _context.Booking.Include(b => b.room);
            return View(await demoContext.ToListAsync());
        }
        public async Task<IActionResult> BookingList()
        {
            var demoContext = _context.Booking.Include(b => b.room);
            return View(await demoContext.ToListAsync());
        }
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
        //Details Khach hang
        public async Task<IActionResult> Details1(int? id)
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,CheckInDate,CheckOutDate,Adult,Child,Request,RoomId")] Booking booking)
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", booking.RoomId);
            return View(booking);
        }
        //Edit1
        public async Task<IActionResult> Edit1(int? id)
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "RoomType", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(int id, [Bind("Id,Name,Email,Phone,CheckInDate,CheckOutDate,Adult,Child,Request,RoomId")] Booking booking)
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
                return RedirectToAction(nameof(BookingList));
            }
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
            return _context.Booking.Any(e => e.Id == id);
        }

        //Hủy phòng
        // GET: Bookings/Cancel/5
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

            return RedirectToAction(nameof(BookingList));
        }



    }
}