using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace la_mia_pizzeria_static
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<PizzaCategory>? Categories { get; set; }
        public List<SelectListItem>? Ingredients { get; set; }
        public List<string>? SelectedIngredients { get; set; }
    }
}