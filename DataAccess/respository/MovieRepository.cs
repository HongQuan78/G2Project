using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public class MovieRepository: IMovieRepository
    {
        public Movie GetMovieByID(int movieId) => MovieDAO.Instance.GetMovieByID(movieId);
        public IEnumerable<Movie> GetMovies() => MovieDAO.Instance.GetMovieList();
        public void InsertMovie(Movie movie) => MovieDAO.Instance.AddNewMovie(movie);
        public void DeleteMovie(int movieId) => MovieDAO.Instance.RemoveMovie(movieId);
        public void UpdateMovie(Movie movie) => MovieDAO.Instance.UpdateMovie(movie);

    }
}
