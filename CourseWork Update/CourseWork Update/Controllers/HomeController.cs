using CourseWork_Update.Models;
using Microsoft.AspNetCore.Mvc;
using CourseWork_Update.Data;
using Microsoft.AspNetCore.Identity;

namespace CourseWork_Update.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult MainPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Terms()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AboutBank()
        {
            return View();
        }

    }
}