namespace PickABookWeb.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publisher is required.")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Edition is required.")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1000, 9999, ErrorMessage = "Year must be a 4-digit number.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        public string Condition { get; set; }

        public string Description { get; set; } // Nullable

        [Required(ErrorMessage = "Picture is required.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "SellerId is required.")]
        public string SellerId { get; set; }

        [ForeignKey("SellerId")]
        public DefaultUser defaultUser { get; set; }
    }
}
