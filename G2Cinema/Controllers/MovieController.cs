using CinemaObject;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace G2Cinema.Controllers
{
    public class MovieController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public MovieController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }
        // GET: MovieController
        public async Task<IActionResult> Index()
        {
            response = await client.GetAsync("movies");
            var json = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(json);
            return View(movieList);
        }


        public async Task<ActionResult> GetMovieByGenres(string genres)
        {
            response = await client.GetAsync("movies");
            var json = await response.Content.ReadAsStringAsync();
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(json);
            TempData["Genres"] = genres;
            return View(movieList);
        }

    }
}
