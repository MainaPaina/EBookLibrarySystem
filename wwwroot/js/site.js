// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// RatingStars for EBooks
function setFeedbackRating(starRating)
{
    console.log("HEI du e på rett plass!!!");
    document.getElementById("feedbackStarRating").value = starRating;
    for (let numberOfStars = 1; numberOfStars < 6; numberOfStars++)
    {
        let starPicture = "/images/icons8-star-96-clear.png";
        if (numberOfStars <= starRating)
        {
            starPicture = "/images/icons8-star-96-full.png";
        }
        document.getElementById("feedbackStar" + numberOfStars).setAttribute("src", starPicture);
    }
}