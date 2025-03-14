using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Library_Management_System.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserRating { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        public Book? Book { get; }
    }
}
