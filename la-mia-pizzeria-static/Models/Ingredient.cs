﻿namespace la_mia_pizzeria_static
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Pizza> Pizze { get; set; }
    }
}
