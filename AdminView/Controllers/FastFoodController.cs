using CinemaObject;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;

namespace AdminView.Controllers
{
    public class FastFoodController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public FastFoodController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }
        // GET: FastFoodController
        public async Task<IActionResult> Index()
        {
            response = await client.GetAsync("fastfoods");
            string jsonString = await response.Content.ReadAsStringAsync();
            var fastFoodList = JsonConvert.DeserializeObject<List<FastFood>>(jsonString);
            return View(fastFoodList);
        }

        // GET: FastFoodController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"fastfoods/{id}");
            var fastFood = await response.Content.ReadFromJsonAsync<FastFood>();
            if (fastFood == null)
            {
                return NotFound();
            }
            return View(fastFood);
        }

        // GET: FastFoodController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FastFoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FastFood fastFood)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    response = await client.PostAsJsonAsync("fastfoods", fastFood);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(fastFood);
            }
        }

        // GET: FastFoodController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"fastfoods/{id}");
            var fastFood = await response.Content.ReadFromJsonAsync<FastFood>();
            if (fastFood == null)
            {
                return NotFound();
            }
            return View(fastFood);
        }

        // POST: FastFoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FastFood fastFood)
        {
            try
            {
                if (id != fastFood.Id)
                    return NotFound();
                if (ModelState.IsValid)
                {
                    // Call the PutAccount API to update the account
                    var apiEndpoint = $"fastfoods/{id}";
                    var json = JsonConvert.SerializeObject(fastFood);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: FastFoodController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"fastfoods/{id}");
            var fastFood = await response.Content.ReadFromJsonAsync<FastFood>();
            if (fastFood == null) { return NotFound(); }

            return View(fastFood);
        }

        // POST: FastFoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                response = await client.DeleteAsync($"fastfoods/{id}");
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
