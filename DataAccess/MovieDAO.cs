using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess
{
    public class MovieDAO
    {
        private static MovieDAO instance = null;
        public static readonly object instanceLock = new object();
        public static MovieDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MovieDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Movie> GetMovieList()
        {
            var movies = new List<Movie>();
            try
            {
                using var context = new CinemaProject_v4Context();
                movies = context.Movies.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movies;
        }

        public Movie GetMovieByID(int movieID)
        {
            Movie movie = null;
            try
            {
                using var context = new CinemaProject_v4Context();
                movie = context.Movies.SingleOrDefault(c => c.Id == movieID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movie;
        }

        public void AddNewMovie(Movie movie)
        {
            try
            {
                Movie _movie = GetMovieByID(movie.Id);
                if (_movie == null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Movies.Add(movie);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The movie is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMovie(Movie movie)
        {
            try
            {
                Movie _movie = GetMovieByID(movie.Id);
                if (_movie != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Movies.Update(movie);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The movie does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveMovie(int movieID)
        {
            try
            {
                Movie movie = GetMovieByID(movieID);
                if (movie != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.Movies.Remove(movie);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The movie does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
