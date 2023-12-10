using System.Diagnostics;
using System.Text.Json;
using ders.Models;
using Microsoft.AspNetCore.Mvc;

namespace ders.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var products = new List<ProductDTO>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5233/api/products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<ProductDTO>>(apiResponse);

                }
            }
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}