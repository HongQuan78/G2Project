using CinemaObject;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;

namespace AdminView.Controllers
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


        // GET: MovieController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"movies/{id}");

            var movie = await response.Content.ReadFromJsonAsync<Movie>();
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    response = await client.GetAsync("movies");
                    response = await client.PostAsJsonAsync("movies", movie);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(movie);
            }
        }

        // GET: MovieController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"movies/{id}");
            var movie = await response.Content.ReadFromJsonAsync<Movie>();
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            try
            {
                if (id != movie.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    // Call the PutAccount API to update the account
                    var apiEndpoint = $"movies/{id}";
                    var json = JsonConvert.SerializeObject(movie);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(apiEndpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"movies/{id}");
            var movie = await response.Content.ReadFromJsonAsync<Movie>();
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                response = await client.DeleteAsync($"movies/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
