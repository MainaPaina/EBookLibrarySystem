using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Book_Library_Management_System.Data;
using Book_Library_Management_System.Models;
using Book_Library_Management_System.Models.Entities;

namespace Book_Library_Management_System.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDBContext dbContext;

        public BooksController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET : Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST : Add
        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel viewModel)
        {
            var book = new Book
            {
                BookTitle = viewModel.BookTitle,
                Genre = viewModel.Genre,
                Author = viewModel.Author,
                Subscribed = viewModel.Subscribed,
            };

            //Add Picture
            if (viewModel.BookPicture is not null)
            {
                using var memoryStream =  new MemoryStream();
                await viewModel.BookPicture.CopyToAsync(memoryStream);
                book.BookPicture = memoryStream.ToArray();
            }

            //Add PDF File
            if(viewModel.PdfFile is not null)
            {
                using var memoryStream = new MemoryStream();
                await viewModel.PdfFile.CopyToAsync(memoryStream);
                book.PdfFile = memoryStream.ToArray();
                book.PdfFileName = viewModel.PdfFile.FileName;
            }

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("List", "Books");
        }

        // GET : List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var books = await dbContext.Books.ToListAsync();
            return View(books);
        }

        // GET : Download PDF
        [HttpGet]
        public async Task<IActionResult> DownloadPdf(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book == null || book.PdfFile == null)
            {
                return NotFound();
            }
            return File(book.PdfFile, "application/pdf", book.PdfFileName);
        }

        // GET : Preview
        [HttpGet]
        public async Task<IActionResult> PreviewPdf(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if (book == null || book.PdfFile == null)
            {
                return NotFound();
            }
            return File(book.PdfFile, "application/pdf");
        }

        // GET : Edit
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await dbContext.Books.FindAsync(id);
            if(book is not null)
            {
                var viewModel = new AddBookViewModel
                {
                    Id = book.BookId,
                    BookTitle = book.BookTitle,
                    Genre = book.Genre,
                    Author = book.Author,
                    Subscribed = book.Subscribed,
                    ExistingBookPicture = book.BookPicture,
                    ExistingPdfFile = book.PdfFile,
                    ExistingPdfFileName = book.PdfFileName

                };

                return View(viewModel);
            }
            
            return(NotFound());
        }

        // POST : Edit
        [HttpPost]
        public async Task<IActionResult> Edit(AddBookViewModel viewModel)
        {
            var book = await dbContext.Books.FindAsync(viewModel.Id);
            if (book is not null)
            {
                book.BookTitle = viewModel.BookTitle;
                book.Genre = viewModel.Genre;
                book.Author = viewModel.Author;
                book.Subscribed = viewModel.Subscribed;

                //Update Picture
                if (viewModel.BookPicture != null)
                {
                    using var memoryStream = new MemoryStream();
                    await viewModel.BookPicture.CopyToAsync(memoryStream);
                    book.BookPicture = memoryStream.ToArray();
                }

                //update PDF
                if(viewModel.PdfFile != null)
                {
                    using var memoryStream = new MemoryStream();
                    await viewModel.PdfFile.CopyToAsync(memoryStream);
                    book.PdfFile = memoryStream.ToArray();
                    book.PdfFileName = viewModel.PdfFile.FileName;
                }
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Books");
        }

        // POST : Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Book viewModel)
        {
            var book = await dbContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.BookId
            == viewModel.BookId);
            if (book is not null)
            {
                dbContext.Books.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Books");
        }
    }
}
