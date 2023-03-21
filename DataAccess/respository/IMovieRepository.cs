using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMovies();
        Movie GetMovieByID(int movieId);
        void DeleteMovie(int movieId);
        void InsertMovie(Movie movie);
        void UpdateMovie(Movie movie);
    }
}
