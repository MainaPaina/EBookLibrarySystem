using Book_Library_Management_System.Models.Entities;

namespace Book_Library_Management_System.Models
{
    public class AddRatingModel
    {
        public string Name { get; set; }
        public int Rating { get; set; }
        public Book? Book { get; set; }
    }
}
