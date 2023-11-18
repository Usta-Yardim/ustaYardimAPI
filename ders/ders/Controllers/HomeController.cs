using System.Diagnostics;
using ders.Models;
using Microsoft.AspNetCore.Mvc;

namespace ders.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}