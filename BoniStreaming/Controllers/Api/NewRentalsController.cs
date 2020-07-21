using BoniStreaming.Dtos;
using BoniStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BoniStreaming.Controllers
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext db;

        public NewRentalsController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDTO newRental)
        {
            // if customer == null
            if (newRental.CustomerID == 0)
                return BadRequest("CustomerId is not valid");

            // if there's no movies in the DTO
            if (newRental.MovieIds == null)
                return BadRequest("MovieIds are missing or not valid ");

            foreach (int movieId in newRental.MovieIds) {

                // if one (or more) movie(s) is invalid
                if (movieId == 0 || db.Movies.Find(movieId) == null)
                    return BadRequest("There is no movie with movieId " + movieId);

                // check if one or more movie(s) is unavailable
                if (db.Movies.Find(movieId).NumberAvailable <= 0)
                    return BadRequest("Movie is not available");

                Rental rental = new Rental();           

                rental.Customer = db.Customers.Find(newRental.CustomerID);
                rental.Movie = db.Movies.Find(movieId);
                rental.DateRented = DateTime.Now;

                db.Rentals.Add(rental);
                db.SaveChanges();

                // diminish the number of available copies by one
                db.Movies.Find(movieId).NumberAvailable = (byte) (db.Movies.Find(movieId)
                    .NumberAvailable - 1);
                db.SaveChanges();

                // somewhere 1 shoulbd be added to NumberAvailable when the movie is returned

            }
            
            return Ok();
        }
    }
}
