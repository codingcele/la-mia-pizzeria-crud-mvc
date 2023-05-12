using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace la_mia_pizzeria_static
{
    public class PizzeriaController : Controller
    {
        private ICustomLogger _logger;

        public PizzeriaController(ICustomLogger logger)
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
                Pizza pizzaById = context.Pizza.Where(m => m.Id == id).Include(pizza => pizza.PizzaCategory).FirstOrDefault();
                
                if (pizzaById == null)
                {
                    return NotFound($"La pizza con id {id} non è stata trovata.");
                }
                else
                {
                    return View("Details", pizzaById);
                } 
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext context = new PizzeriaContext())
                {
                    List<Ingredient> ingredients = context.Ingredients.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();

                    List<PizzaCategory> categories = context.PizzaCategories.ToList();
                 
                    foreach (Ingredient ingredient in ingredients)
                    {
                        listIngredients.Add(new SelectListItem()
                            {
                                Text = ingredient.Name,
                                Value = ingredient.Id.ToString(),
                            });
                    }

                    data.Categories = categories;
                    data.Ingredients = listIngredients;

                    return View("Create", data);
                }
            }
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaToCreate = new Pizza();
                pizzaToCreate.Image = data.Pizza.Image;
                pizzaToCreate.Name = data.Pizza.Name;
                pizzaToCreate.Description = data.Pizza.Description;
                pizzaToCreate.Price = data.Pizza.Price;

                pizzaToCreate.PizzaCategoryId = data.Pizza.PizzaCategoryId;



                pizzaToCreate.Ingredients = new List<Ingredient>();



                if (data!= null && data.SelectedIngredients != null)
                {
                    foreach (string selectedIngredientsId in data.SelectedIngredients)
                    {
                        int selectedIntIngredientId = int.Parse(selectedIngredientsId);
                        Ingredient ingredient = context.Ingredients.Where(m => m.Id == selectedIntIngredientId).FirstOrDefault();
                        pizzaToCreate.Ingredients.Add(ingredient);
                    }
                }

                context.Pizza.Add(pizzaToCreate);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                List<PizzaCategory> categories = context.PizzaCategories.ToList();
                List<Ingredient> ingredients = context.Ingredients.ToList();

                PizzaFormModel model = new PizzaFormModel();
                

                List<SelectListItem> listIngredients = new List<SelectListItem>();

                foreach (Ingredient ingredient in ingredients)
                {
                    listIngredients.Add(new SelectListItem()
                    {
                        Text = ingredient.Name, Value = ingredient.Id.ToString()
                    });
                }

                model.Pizza = new Pizza();
                model.Ingredients = listIngredients;
                model.Categories = categories;
                

                return View("Create", model);
            }
        }
          

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaToDelete = context.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
            
                if (pizzaToDelete != null)
                {
                    context.Pizza.Remove(pizzaToDelete);
                    context.SaveChanges();
                    _logger.WriteLog("Pizza eliminata!");

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public IActionResult Update(int id) { 
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaToEdit = context.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaToEdit == null)
                {
                    return NotFound();
                }
                else
                {
                    List<PizzaCategory> categories = context.PizzaCategories.ToList();

                    PizzaFormModel model = new PizzaFormModel();
                    model.Pizza = pizzaToEdit;
                    model.Categories = categories;

                    return View(model);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext context = new PizzeriaContext())
                {
                    List<PizzaCategory> categories = context.PizzaCategories.ToList();
                    data.Categories = categories;

                    return View("Update", data);
                }
                
            }
            using (PizzeriaContext context = new PizzeriaContext())
            {
                Pizza pizzaToEdit = context.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit != null)
                {
                    pizzaToEdit.Image = data.Pizza.Image;
                    pizzaToEdit.Name = data.Pizza.Name;
                    pizzaToEdit.Description = data.Pizza.Description;
                    pizzaToEdit.Price = data.Pizza.Price;

                    pizzaToEdit.PizzaCategoryId = data.Pizza.PizzaCategoryId;

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

    }
}