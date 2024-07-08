using Demo.Data;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {

        private readonly DemoContext _context;

        public HomeController(DemoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Room.ToList());
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Booking()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View(_context.Service.ToList());
        }
        public IActionResult Rooms()
        {
            return View(_context.Room.ToList());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
