using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickABookWeb.Models
{
    public class UserBook
    {
      

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserBookId { get; set; }

        
        [ForeignKey("Seller")]
        public Guid Id { get; set; }  // Foreign key to the User entity

        [ForeignKey("Book")]
        public int BookId { get; set; }  // Foreign key to the Book entity

        public IdentityUser Seller { get; set; }  // Navigation property to User entity

        public Book Book { get; set; }  // Navigation property to Book entity

        // You can add additional properties specific to the relationship, such as purchase date, ownership status, etc.
    }
}
