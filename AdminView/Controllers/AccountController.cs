using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using System.Security.Principal;

namespace AdminView.Controllers
{
    public class AccountController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public AccountController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }


        // GET: AccountController
        public async Task<ActionResult> IndexAsync()
        {
            response = await client.GetAsync("accounts");

            string jsonString = await response.Content.ReadAsStringAsync();
            var accountList = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            return View(accountList);
        }

        // GET: AccountController/Details/5
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"accounts/{id}");
            var acc = new Account();
            acc = await response.Content.ReadFromJsonAsync<Account>();
            if (acc == null)
            {
                return NotFound();
            }

            return View(acc);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account)
        {
            try
            {
                response = await client.GetAsync("acounts");
                if (ModelState.IsValid)
                {
                    response = await client.PostAsJsonAsync("accounts", account);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.InnerException.Message;
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"accounts/{id}");
            var acc = new Account();
            acc = await response.Content.ReadFromJsonAsync<Account>();
            if (acc == null)
            {
                return NotFound();
            }

            return View(acc);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Account account)
        {
            try
            {
                // Validate the ID parameter and the Account object
                if (id != account.Id || !ModelState.IsValid)
                {
                    return NotFound();
                }

                // Call the PutAccount API to update the account
                var apiEndpoint = $"accounts/{id}";
                var json = JsonConvert.SerializeObject(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await client.PutAsync(apiEndpoint, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to update account with ID={id}. Status code: {response.StatusCode}");
                }

                // Redirect to the index action of the controller
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"accounts/{id}");
            var account = new Account();
            account = await response.Content.ReadFromJsonAsync<Account>();
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Call the DeleteAccount API to delete the account
                var apiEndpoint = $"accounts/{id}";
                
                 response = await client.DeleteAsync(apiEndpoint);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to delete account with ID={id}. Status code: {response.StatusCode}");
                }

                // Redirect to the index action of the controller
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
