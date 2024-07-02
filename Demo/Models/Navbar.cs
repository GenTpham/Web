using Demo.Data;
using Microsoft.AspNetCore.Mvc;
namespace Demo.Models
{
    public class Navbar: ViewComponent
    {
        private readonly DemoContext _context;

        public Navbar(DemoContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Category.ToList());
        }
    }
}
