using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PickABookWeb.Models;
using System.Collections.Generic;

namespace PickABookWeb.Data
{
    public class DBContext : IdentityDbContext<DefaultUser>
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } // DbSet for the Book entity


       
    }
}
