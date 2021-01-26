using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OverviewASPNetCore.Models;
using System.Collections.Generic;
using System.Linq;
using Training.Context;

namespace Training.Controllers
{
    public class EducationController : Controller
    {
        private readonly MyContext myContext;

        public EducationController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IActionResult Index()
        {
           
            var educations = myContext.Educations
                .Where(x => x.IsAvailable == true)
                .ToList();
            return View(educations);
        }

        public IActionResult Create()
        {
            List<University> universityList = new List<University>();
            universityList = (from product in myContext.Universities select product).ToList();
            universityList.Insert(0, new University { Id = 0, Name = "Select University" });
            ViewBag.ListofUniversity = universityList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Education education)
        {
            
            myContext.Educations.Add(education);
           
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            return View();
        }

        public IActionResult Edit(int? id)
        {
            List<University> universityList = new List<University>();
            universityList = (from product in myContext.Universities select product).ToList();
            universityList.Insert(0, new University { Id = 0, Name = "Select University" });
            ViewBag.ListofUniversity = universityList;

            if (id == null)
            {
                return View();
            }
            var result = myContext.Educations.Find(id);
            if (result == null)
            {
                return View();
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Education education)
        {
            if (id == null)
            {
                return View();
            }
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                get.Degree = education.Degree;
                get.GPA = education.GPA;
                get.University_Id = education.University_Id;
                get.IsAvailable = education.IsAvailable;
                myContext.Entry(get).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result <= 0)
                    return View();
                return RedirectToAction(nameof(Index));
                return View();
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            var get = myContext.Educations.Find(id);
            if (get != null)
            {
                myContext.Educations.Remove(get);
                var result = myContext.SaveChanges();
                if (result > 0)
                    return Json(result);
            }
            return View(id);
        }

}
}
