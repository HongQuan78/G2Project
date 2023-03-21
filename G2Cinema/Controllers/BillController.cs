using CinemaObject;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace G2Cinema.Controllers
{
    public class BillController : Controller
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response;
        public BillController()
        {
            client.BaseAddress = new Uri(new getAPIPort().APIPORT);
        }

        // GET: FastFoodController/Details/5
        // POST: FastFoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bill bill)
        {
            try
            {
                response = await client.GetAsync($"bills/{bill.Id}");
                Bill _bill = await response.Content.ReadFromJsonAsync<Bill>();

                response = await client.GetAsync($"fastfoods/{bill.FastFood}");
                FastFood Food = await response.Content.ReadFromJsonAsync<FastFood>();
                response = await client.GetAsync($"fastfoods/{bill.Drink}");
                FastFood Drink = await response.Content.ReadFromJsonAsync<FastFood>();
                response = await client.GetAsync($"tickets/{bill.TicketId}");
                Ticket ticket = await response.Content.ReadFromJsonAsync<Ticket>();

                bill.TicketType = ticket.TicketType;
                bill.Total = 0;

                bill.BookingDate = DateTime.Today;
                bill.Total = ticket.Price + Food.Price * bill.QuantityFastfood + Drink.Price * bill.QuantityDrink;
                Console.WriteLine(bill.TicketId + " " + bill.IdMovie + " " + bill.FastFood + " " + bill.SeatNum + " " + bill.QuantityDrink + " " + bill.QuantityFastfood + " " + bill.TicketType + " " + bill.Total + " " + bill.Drink);
                using var context = new CinemaProject_v4Context();
                response = await client.GetAsync("bills");
                response = await client.PostAsJsonAsync("bills", bill);

                return Redirect("/Home/Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("Booking", "Booking");
            }
        }

    }
}
