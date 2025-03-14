namespace Book_Library_Management_System.Models
{
    public class AddBookViewModel
    {
        public Guid Id { get; set; }
        public string BookTitle { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public bool Subscribed { get; set; }
        public IFormFile? BookPicture { get; set; }
        public byte[]? ExistingBookPicture { get; set; }
        public IFormFile PdfFile { get; set; }
        public string ExistingPdfFileName { get; set; }
        public byte[]? ExistingPdfFile { get; set; }

        public IFormFile ExistingProfilePicture { get; set; }

    }
}
