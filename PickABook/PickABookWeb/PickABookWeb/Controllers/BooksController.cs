using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PickABookWeb.Data;
using PickABookWeb.Migrations;
using PickABookWeb.Models;


namespace PickABookWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly DBContext _context;
        IWebHostEnvironment hostEnvironment;
        
        
        public BooksController(DBContext context,IWebHostEnvironment hc)
        {
           
            _context = context;
            hostEnvironment = hc;
        }

        // GET: Books
        //public async Task<IActionResult> Index(Book Book)
        //{

        //    var dBContext = _context.Books.Include(b => b.defaultUser);
        //    return View(await dBContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(string search)
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
        public async Task<IActionResult> BuyBook(string search)
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

        public async Task<IActionResult> ContactSeller()
        {

          
            return View();
        }
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.defaultUser)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        public async Task<IActionResult> BuyBookDetails(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.defaultUser)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        //POST: Books/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(bookviewsmodel book1)
        {
            
            String filename = "";
            if (book1.Photo != null)
            {
                String uploadfolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                filename = Guid.NewGuid().ToString() + "_" + book1.Photo.FileName;
                String filepath = Path.Combine(uploadfolder, filename);
                book1.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
            }
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Id", book1.SellerId);
            Book b = new Book()
            {
                Title = book1.Title,
                Author = book1.Author,
                ISBN = book1.ISBN,
                Publisher = book1.Publisher,
                Edition = book1.Edition,
                Year = book1.Year,
                Price = book1.Price,
                Condition = book1.Condition,
                Description = book1.Description,
                Image = filename,
                SellerId = currentUserId
            };
             await _context.Books.AddAsync(b);
            await _context.SaveChangesAsync();
            ViewBag.success = "Record Added";
            return View("Index");
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("BookId,Title,Author,ISBN,Publisher,Edition,Year,Price,Condition,Description,Picture,SellerId")] Book book)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(book);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Id", book.SellerId);
        //    return View(book);
        //}




        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Id", book.SellerId);

            // Create an instance of bookviewsmodel and populate it with data from the Book model
            var bookViewModel = new bookviewsmodel
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                Publisher = book.Publisher,
                Edition = book.Edition,
                Year = book.Year,
                Price = book.Price,
                Condition = book.Condition,
                Description = book.Description,
                SellerId = book.SellerId // Populate the SellerId
            };

            return View(bookViewModel);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bookviewsmodel book1)
        {
            if (id != book1.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the book from the database
                    var book = await _context.Books.FindAsync(id);

                    // Update the book's properties based on the model's values
                    book.Title = book1.Title;
                    book.Author = book1.Author;
                    book.ISBN = book1.ISBN;
                    book.Publisher = book1.Publisher;
                    book.Edition = book1.Edition;
                    book.Year = book1.Year;
                    book.Price = book1.Price;
                    book.Condition = book1.Condition;
                    book.Description = book1.Description;
                    book.SellerId = book1.SellerId; // Update the SellerId

                    // Check if a new photo was uploaded and update the file path
                    if (book1.Photo != null)
                    {
                        string uploadFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                        string filename = Guid.NewGuid().ToString() + "_" + book1.Photo.FileName;
                        string filepath = Path.Combine(uploadFolder, filename);
                        book1.Photo.CopyTo(new FileStream(filepath, FileMode.Create));
                        book.Image = filename; // Update the photo path in the book entity
                    }

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book1.BookId))
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

            ViewData["SellerId"] = new SelectList(_context.Users, "Id", "Id", book1.SellerId);
            return View(book1);
        }


        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.defaultUser)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'DBContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }

        public IActionResult ContactSeller(string sellerId)
        {
            // Retrieve the seller's email using the sellerId from your data source
            var seller = _context.Users.FirstOrDefault(u => u.Id == sellerId);

            if (seller != null)
            {
                ViewBag.SellerEmail = seller.Email; // Pass the seller's email to the view
            }
            else
            {
                ViewBag.SellerEmail = null;
            }

            return View();
        }

    }
}
