using la_mia_pizzeria_static.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace la_mia_pizzeria_static
{
    public class PizzeriaController : Controller
    {
        private readonly ILogger<PizzeriaController> _logger;

        public PizzeriaController(ILogger<PizzeriaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizze = db.Pizza.ToList();

                return View(pizze);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaById = context.Pizza.Where(m => m.Id == id).FirstOrDefault();
                return View("Details", pizzaById);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaToCreate = new Pizza();
                pizzaToCreate.Image = data.Image;
                pizzaToCreate.Name = data.Name;
                pizzaToCreate.Ingredients = data.Ingredients;
                pizzaToCreate.Price = data.Price;
                context.Pizza.Add(pizzaToCreate);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

    }
}
