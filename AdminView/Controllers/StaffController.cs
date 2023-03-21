using CinemaObject;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;

namespace AdminView.Controllers
{
    public class StaffController : Controller
    {

        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public StaffController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }
        // GET: MovieController
        public async Task<IActionResult> IndexAsync()
        {
            response = await client.GetAsync("staffs"); 
            var staffList = new List<staff>();
            string json = await response.Content.ReadAsStringAsync();
            staffList = JsonConvert.DeserializeObject<List<staff>>(json);
            return View(staffList);
        }

        // GET: MovieController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
             
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"staffs/{id}");
            var staff = await response.Content.ReadFromJsonAsync<staff>();
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // GET: MovieController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    response = await client.PostAsJsonAsync("staff", staff);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(staff);
            }
        }

        // GET: MovieController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"staffs/{id}");
            var staff = await response.Content.ReadFromJsonAsync<staff>();
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, staff staff)
        {
            try
            {
                if (id != staff.Id)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    // Call the PutAccount API to update the account
                    var apiEndpoint = $"staffs/{id}";
                    var json = JsonConvert.SerializeObject(staff);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(apiEndpoint, content);
                }
                return RedirectToAction("Index");
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
            response = await client.GetAsync($"staffs/{id}");
            var staff = await response.Content.ReadFromJsonAsync<staff>();
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                response = await client.DeleteAsync($"staffs/{id}");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
