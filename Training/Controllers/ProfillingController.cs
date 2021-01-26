using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OverviewASPNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.Context;

namespace Training.Controllers
{
    public class ProfillingController : Controller
    {
        private readonly MyContext myContext;
        public ProfillingController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IActionResult Index()
        {
            var Profilling = myContext.Profilings
                .Where(x => x.IsAvailable == true)
                .ToList();
            return View(Profilling);
        }

        public IActionResult Create()
        {
            List<Education> educationList = new List<Education>();
            educationList = (from product in myContext.Educations select product).ToList();
            educationList.Insert(0, new Education { Id = 0, Degree = "Select Education Id" });
            ViewBag.ListofEducation = educationList;
            List<Account> accountList = new List<Account>();
            accountList = (from product in myContext.Accounts select product).ToList();
            accountList.Insert(0, new Account { NIK = "Select NIK" });
            ViewBag.ListofAccount = accountList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Profiling profilling)
        {
            myContext.Profilings.Add(profilling);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult Edit(int? id)
        {
            List<Education> educationList = new List<Education>();
            educationList = (from product in myContext.Educations select product).ToList();
            educationList.Insert(0, new Education { Id = 0, Degree = "Select Education Id" });
            ViewBag.ListofEducation = educationList;
            List<Account> accountList = new List<Account>();
            accountList = (from product in myContext.Accounts select product).ToList();
            accountList.Insert(0, new Account { NIK = "Select NIK" });
            ViewBag.ListofAccount = accountList;

            if (id == null)
            {
                return View();
            }
            var result = myContext.Profilings.Find(id);
            if (result == null)
            {
                return View();
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Profiling profiling)
        {
            if (id == null)
            {
                return View();
            }
            var get = myContext.Profilings.Find(id);
            if (get != null)
            {
                get.NIK = profiling.NIK;
                get.Education_Id = profiling.Education_Id;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result <= 0)
                return RedirectToAction(nameof(Index));
                return View();
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var get = myContext.Profilings.Find(id);
            if (get != null)
            {
                myContext.Profilings.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
            }
            return View(id);
        }

    }
}
