using CinemaObject;
using G2Cinema.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace G2Cinema.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client = new HttpClient();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {

            HttpResponseMessage response = await client.GetAsync("movies");
            ViewModel viewModel = new ViewModel();
            viewModel.movieList = new List<Movie>();
            List<Movie> movies = new List<Movie>();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                movies = JsonConvert.DeserializeObject<List<Movie>>(jsonString);
                viewModel.movieList = movies;
            }
            int count = 0;
            var moList = new List<Movie>();
            foreach (var i in movies)
            {
                if (count == 6)
                {
                    break;
                }

                moList.Add(i);
                count++;
            }
            viewModel.mostPopularMovies = moList;

            return View(viewModel);
        }


        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search()
        {
            string searchMovie = Request.Form["Search"] + "";
            var response = await client.GetAsync($"movies/search/{searchMovie}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }
            string jsonString = await response.Content.ReadAsStringAsync();
            List<Movie> movieList = new List<Movie>();
            movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonString);
            if (movieList.Count == 0)
            {
                TempData["NotFoundMovie"] = "Sorry, HighCinema has not this movie!";
                return Redirect("/Home/Index");
            }

            return View(movieList);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            var response = await client.GetAsync($"accounts/{account.UserName}/{account.Password}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(account);
            }

            string jsonString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<Account>(jsonString);
            if (obj == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(account);
            }

            HttpContext.Session.SetString("Username", obj.UserName.ToString());
            HttpContext.Session.SetString("Password", obj.Password.ToString());
            HttpContext.Session.SetString("Role", obj.Role.ToString());
            if (obj.Role == 1)
            {
                return Redirect("https://localhost:44303"); ;
            }
            return RedirectToAction("Index");
        }



        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(Account account)
        {
            try
            {
                var response = await client.GetAsync($"accounts");
                if (ModelState.IsValid)
                {
                    await client.PostAsJsonAsync("Accounts", account);
                }
                TempData["Register"] = "Register Successfully!";
                return Redirect("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Console.WriteLine(ex.Message);
                return View();
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
