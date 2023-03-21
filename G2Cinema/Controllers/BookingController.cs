using CinemaObject;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using G2Cinema.Models;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace G2Cinema.Controllers
{
    public class BookingController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public BookingController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }
        

        [HttpGet]
        public async Task<IActionResult> Booking(int id)
        {
            if(HttpContext.Session.GetString("Role") == null)
            {
                TempData["NeedLoginToBooking"] = "Please login to your account for booking ticket";
                return Redirect("/Home/Login");
            }


            BookingModel booking = new BookingModel();


            response = await client.GetAsync($"movies/{id}");
            booking.Movie = await response.Content.ReadFromJsonAsync<Movie>();

            response = await client.GetAsync("FastFoods");
            booking.Food = JsonConvert.DeserializeObject<List<FastFood>>(await response.Content.ReadAsStringAsync());
            response = await client.GetAsync("Tickets");
            booking.Tickets = JsonConvert.DeserializeObject<List<Ticket>>(await response.Content.ReadAsStringAsync());

            var listFood = new List<FastFood>();
            var listDrink = new List<FastFood>();

            foreach (var food in booking.Food)
            {
                if (food.Name == "Coca")
                {
                    listDrink.Add(food);
                }
                else
                {
                    listFood.Add(food);
                }
            }

            booking.Drink = listDrink;
            booking.Food = listFood;

            booking.Tickets = JsonConvert.DeserializeObject<List<Ticket>>(await response.Content.ReadAsStringAsync());

            return View(booking);
        }

    }
}
