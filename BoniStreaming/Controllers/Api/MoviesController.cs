using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using BoniStreaming.Dtos;
using BoniStreaming.Models;

namespace BoniStreaming.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext db;

        public MoviesController()
        {
            db = new ApplicationDbContext();
        }

        // GET all api/movies/
        public IHttpActionResult GetMovies(string query = null)
        {
            if (!db.Movies.ToList().Any())
            {
                return NotFound();
            }

            var moviesQuery = db.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery
                    .Where(m => m.Name.Contains(query));
            }
            
            var moviesDTO = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDTO);
        }

        // GET 1 api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            Movie movieInDb = db.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
            {
                BadRequest();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movieInDb));
        }

        // POST api/movies/
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (movieDto == null)
            {
                return BadRequest();
            }

            Movie movieToCreate = Mapper.Map<MovieDto, Movie>(movieDto);

            db.Movies.Add(movieToCreate);
            db.SaveChanges();

            movieDto.Id = movieToCreate.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        // PUT api/movies/id
        [HttpPut]
        public IHttpActionResult EditMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            if (movieDto == null)
            {
                BadRequest();
            }

            Movie movieInDb = db.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
            {
                BadRequest();
            }

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            db.SaveChanges();

            movieDto.Id = id;

            return Ok(movieDto);
        }

        // DELETE api/customer/id
        public IHttpActionResult DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = db.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movieInDb);
            db.SaveChanges();

            return Ok(db.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }
    }
}