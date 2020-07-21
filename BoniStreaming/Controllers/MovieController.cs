using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;
using BoniStreaming.Models;
using BoniStreaming.ViewModels;

namespace BoniStreaming.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movie
        public ActionResult Index()
        {
            IEnumerable<Movie> movieList = db.Movies.ToList();

            if(User.IsInRole(RoleName.CanManageMovies))
                return View("List", movieList );

            return View("ReadOnlyList", movieList);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            Movie movieInDb = db.Movies.Find(id);

            if (movieInDb == null)
            {
                return RedirectToAction("Index");
            }

            var movieGenreId = movieInDb.GenreId;
            string movieGenreName ="";
            IEnumerable<Genre> GenresInDB = db.Genres.ToList();

            foreach (Genre genre in GenresInDB)
            {
                if (genre.Id == movieGenreId)
                {
                    movieGenreName = genre.Name;
                }
            }

            MovieFormViewModel viewModel = new MovieFormViewModel()
            {
                movie = movieInDb,
                genreName = movieGenreName,
                genres = GenresInDB
            };

            return View("Details", viewModel);
        }

        // GET: Movie/Create
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create()
        {
            IEnumerable<Genre> genreList = db.Genres.ToList();
            
            MovieFormViewModel viewModel = new MovieFormViewModel()
            {
                genres = genreList,
                movie = new Movie()
            };

            return View("MovieForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CanManageMovies")]
        [HttpPost] 
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                Create();
            }
            
            if (movie.Id == 0)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }
            else
            {
                Movie movieInDb = db.Movies.Find(movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.NumberInStock = movie.NumberInStock;

                db.SaveChanges();
            }


            return RedirectToAction("Index");

        }

        /*
         * // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        */

        // GET: Movie/Edit/5
        [Authorize(Roles = "CanManageMovies")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Movie movieInDb = db.Movies.Find(id);
            
            if (movieInDb == null)
            {
                return RedirectToAction("Index");
            }

            IEnumerable<Genre> genreList = db.Genres.ToList();
            string movieGenre = "";

            foreach (Genre genre in genreList)
            {
                if (genre.Id == movieInDb.GenreId)
                {
                    movieGenre = genre.Name;
                }
            }

            MovieFormViewModel viewModel = new MovieFormViewModel()
            {
                genreName = movieGenre,
                genres = genreList,
                movie = movieInDb
            };

            return View("Edit", viewModel);
        }

        /*
         * // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }
                 */


         //POST: Movie/Delete/5
        [HttpPost]
        [Authorize(Roles = "CanManageMovies")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            int receivedId = id;

            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
