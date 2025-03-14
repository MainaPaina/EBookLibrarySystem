using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Library_Management_System.Models.Entities
{
    [Table(name: "FreeEBooks")]
    public class Book
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public bool Subscribed { get; set; }

        [Display(Name = "Book Picture")]
        public byte[]? BookPicture { get; set; } 
        public byte[]? PdfFile { get; set; }
        public string PdfFileName { get; set; }
        public DateTime PublishedDate { get; set; }

        public double Rating { get; set; }
        public int NumberOfUserRatings { get; set; }
        public List<Rating> Ratings { get; } = [];
    }
}
    