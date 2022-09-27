using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserDB.Models;

namespace UserDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult GetUser(int id)
        {
            return View(Models.Entities.UserDB.CreateFromFile(id));
        }

        [HttpPost]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
               UserDB.Models.Entities.UserDB.WriteToFile(model);
               return RedirectToAction("Index");
            }
            return View(model);

        }




    }
}