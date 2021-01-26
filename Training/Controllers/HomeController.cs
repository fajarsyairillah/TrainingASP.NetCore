using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Training.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Training.Context;
using OverviewASPNetCore.Models;

namespace Training.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext myContext;

        public HomeController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IActionResult Index()
        {
            List<University> universityList = new List<University>();
            universityList = (from product in myContext.Universities select product).ToList();
            universityList.Insert(0, new University { Id = 0 });
            ViewBag.ListofUniversity = universityList;
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
    }
}
