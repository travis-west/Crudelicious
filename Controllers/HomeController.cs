using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crudelicious.Models;

namespace Crudelicious.Controllers
{

    public class HomeController : Controller
    {

        private YourContext dbContext;

        public HomeController(YourContext context)
        {
            dbContext = context;
        }


        public IActionResult Index()
        {
            List<Dish> DishList = dbContext.Dishes.ToList();
            ViewBag.DishList = DishList;

            return View("Index");
        }

        // new dish form
        public IActionResult New()
        {
            return View();
        }

        // post/create the dish action
        [HttpPost("Home/Create")]
        public IActionResult Create(Dish mydish)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(mydish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");         
            }
                return View("New");
        }
        
        [HttpGet("Home/Dish/{dishid}")]
        public IActionResult Show(int dishid)
        {
            List<Dish> onedish = dbContext.Dishes.Where(id => id.DishId == dishid).ToList();
            ViewBag.onedish = onedish;            

            return View("Show");
        }

        [HttpPost("Home/Dish/{dishid}/Delete")]
        public IActionResult Destroy(int dishid)
        {
            Dish RetrievedDish = dbContext.Dishes.SingleOrDefault(id => id.DishId == dishid);
            dbContext.Dishes.Remove(RetrievedDish);
            dbContext.SaveChanges(); 

            return RedirectToAction("Index");
        }

        [HttpPost("Home/Dish/{dishid}/Edit")]
        public IActionResult Edit(int dishid)
        {
            List<Dish> onedish = dbContext.Dishes.Where(id => id.DishId == dishid).ToList();
            ViewBag.onedish = onedish;            

            return View("Edit");
        }

        [HttpPost("Update")]
        public IActionResult Update(int dishid, string name, string chef, int tastiness, int calories, string description)
        {
            List<Dish> retrievedDish = dbContext.Dishes.Where(x => x.DishId == dishid).ToList();
            retrievedDish[0].Name=name;
            retrievedDish[0].Chef=chef;
            retrievedDish[0].Calories=calories;
            retrievedDish[0].Tastiness=tastiness;
            retrievedDish[0].Description=description;
            retrievedDish[0].UpdatedAt= DateTime.Now;
            dbContext.SaveChanges();

            return RedirectToAction("Index");
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
