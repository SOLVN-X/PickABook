using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PickABookWeb.Data;
using PickABookWeb.Models;

namespace PickABookWeb.Controllers
{
    public class Admin : Controller
    {
        private readonly DBContext _context;

        // Constructor to inject the DafContext into the controller.
        public Admin(DBContext context)
        {
            _context = context;
        }
      
        public async Task<ActionResult> Index()
        {

            // Define the date range for the dashboard data (last 7 days).
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            // Retrieve monetary donation data from the database.
            List<Book> selectedBooks = _context.Books.ToList();

            // Calculate the total money donated and store it in ViewBag for display.
            int TotalBooks = selectedBooks.Count;
            ViewBag.TotalBooks = TotalBooks;

            // Retrieve monetary donation data from the database.
            List<DefaultUser> selectedUsers = _context.Users.ToList();

            // Calculate the total money donated and store it in ViewBag for display.
            int TotalUsers = selectedUsers.Count;
            ViewBag.TotalUsers = TotalUsers;
            return View();
        }
        public async Task<ActionResult> Books(string search)
        {
            var query = _context.Books.Include(b => b.defaultUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                // If a search query is provided, filter the results based on the search criteria.
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search) || b.ISBN.Contains(search));
            }

            var model = await query.ToListAsync();

            return View(model);
        }
    }       
}
