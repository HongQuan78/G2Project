using CinemaObject;

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Collections.Generic;

namespace AdminView.Controllers
{
    public class BillController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public BillController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }
        // GET: FastFoodController
        public async Task<IActionResult> Index()
        {
            response = await client.GetAsync("bills");
            var billList = JsonConvert.DeserializeObject<List<Bill>>(await response.Content.ReadAsStringAsync());
            return View(billList);
        }

        // GET: FastFoodController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"bills/{id}");
            var bill = await response.Content.ReadFromJsonAsync<Bill>();
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // POST: FastFoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bill bill)
        {
            try
            {
                if (id != bill.Id)
                { return NotFound(); }
                if (ModelState.IsValid)
                {
                    var apiEndpoint = $"bills/{id}";
                    var json = JsonConvert.SerializeObject(bill);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(apiEndpoint, content);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            return View();
        }

        // GET: FastFoodController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"bills/{id}");
            var bill = await response.Content.ReadFromJsonAsync<Bill>();
            if (bill == null)
                return NotFound();
            return View(bill);
        }

        // POST: FastFoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                response = await client.DeleteAsync($"bills/{id}");
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
