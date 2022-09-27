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
            var t = Models.Entities.UserDB.CreateFromFile(id);
            if (t == null)
            {
                return NotFound();
            }
            else
            {
                return View(t);
            }
        }

        [HttpGet]
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
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
           return  View(Models.Entities.UserDB.CreateFromFile(id));
        }


        [HttpPost]
        public IActionResult UpdateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Models.Entities.UserDB.OverrideUser(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpDelete]
        public IActionResult DeleteUser()
        {
            return View();
        }
        public IActionResult DeleteUser(int id)
        {
            Models.Entities.UserDB.DeleteUser(id);
            return View(id);
        }






    }
}