using Book_Library_Management_System.Data;
using Book_Library_Management_System.Models;
using Book_Library_Management_System.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Book_Library_Management_System.Controllers
{
    public class RatingController : Controller
    {
        private readonly ApplicationDBContext dbContext;

        public RatingController(ApplicationDBContext Context)
        {
            dbContext = Context;
        }


        public IActionResult Add(Guid Id)
        {
            var book = dbContext.Books.Find(Id);
            if (book is not null)
            {
                AddRatingModel ARM = new AddRatingModel()
                {
                    Book = book,
                };
                return View("AddRating", ARM);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Guid Id, AddRatingModel Model)
        {
            var book = dbContext.Books.Find(Id);
            if (book is not null)
            {
                Rating rating = new Rating();
                rating.BookId = Id;
                rating.Name = Model.Name;
                rating.UserRating = Model.Rating;
                dbContext.Ratings.Add(rating);
                await dbContext.SaveChangesAsync();
                book.NumberOfUserRatings = dbContext.Ratings.Where(m => m.BookId == Id).Count();
                book.Rating = dbContext.Ratings.Where(m => m.BookId == Id).Average(m => m.UserRating);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("List", "books");
            }
            return NotFound();
        }
    }
}
