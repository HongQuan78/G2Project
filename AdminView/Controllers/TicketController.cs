using CinemaObject;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdminView.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;

namespace AdminView.Controllers
{
    public class TicketController : Controller
    {

        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public TicketController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }
        // GET: FastFoodController
        public async Task<IActionResult> Index()
        {
            response = await client.GetAsync("tickets");
            var ticketList = JsonConvert.DeserializeObject<List<Ticket>>(await response.Content.ReadAsStringAsync());
            return View(ticketList);
        }

        // GET: FastFoodController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"tickets/{id}");
            var ticket = await response.Content.ReadFromJsonAsync<Ticket>();
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: FastFoodController/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            response = await client.GetAsync("movies");
            ViewCreateTicketModel viewCreateTicketModel = new ViewCreateTicketModel();
            viewCreateTicketModel.Movies = JsonConvert.DeserializeObject<List<Movie>>(await response.Content.ReadAsStringAsync());
            return View(viewCreateTicketModel);
        }

        // POST: FastFoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    response = await client.PostAsJsonAsync("tickets", ticket);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(ticket);
            }
        }

        // GET: FastFoodController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            response = await client.GetAsync($"tickets/{id}");
            var ticket = await response.Content.ReadFromJsonAsync<Ticket>();
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: FastFoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            try
            {
                if (id != ticket.Id)
                    return NotFound();
                if (ModelState.IsValid)
                {
                    var apiEndpoint = $"tickets/{id}";
                    var json = JsonConvert.SerializeObject(ticket);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PutAsync(apiEndpoint, content);
                }
                //Code update vo day 

                return RedirectToAction("Index");
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
            response = await client.GetAsync($"tickets/{id}");
            var ticket = await response.Content.ReadFromJsonAsync<Ticket>();
            if (ticket == null)
                return NotFound();
            return View(ticket);
        }

        // POST: FastFoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                response = await client.DeleteAsync($"tickets/${id}");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Order()
        {
            response = await client.GetAsync("tickets");
            var ticketList = JsonConvert.DeserializeObject<List<Ticket>>(await response.Content.ReadAsStringAsync());
            return View(ticketList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(int? id)
        {
            string txt = "";
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };
            if (Request.Cookies["ticket"] == null)
            {
                txt = id + ":1";
                Response.Cookies.Append("ticket", txt, options);
            }
            else
            {
                txt = Convert.ToString(Request.Cookies["ticket"]);
                Response.Cookies.Append("ticket", "", options);
                string[] t = txt.Split(";");
                var ticketList = new List<Ticket>();
                for (int i = 0; i < t.Length; i++)
                {
                    string[] a = t[i].Split(":");
                    Ticket ticket = new Ticket();
                    response = await client.GetAsync($"tickets/{Convert.ToInt32(a[0])}");
                    ticket = await response.Content.ReadFromJsonAsync<Ticket>();
                    ticket.Quantity = Convert.ToInt32(a[1]);
                    ticketList.Add(ticket);
                }

                int check = 0;

                foreach (Ticket ticket in ticketList)
                {
                    if (ticket.Id == id)
                    {
                        ticket.Quantity++;
                        check = 1;
                        break;
                    }
                }

                txt = ticketList[0].Id + ":" + ticketList[0].Quantity;

                for (int i = 1; i < ticketList.Count; i++)
                {
                    txt += ";" + ticketList[i].Id + ":" + ticketList[i].Quantity;
                }
                if (check == 0)
                {
                    txt += ";" + id + ":1";
                }

                Response.Cookies.Append("ticket", txt, options);
            }
            var tickets = JsonConvert.DeserializeObject<List<Ticket>>(await response.Content.ReadAsStringAsync());
            return View(tickets);
        }
    }
}
