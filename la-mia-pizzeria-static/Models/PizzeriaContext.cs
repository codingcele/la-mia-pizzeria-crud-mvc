using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static
{
    public class PizzeriaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<PizzaCategory> PizzaCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzeriaDb;Integrated Security=True;TrustServerCertificate=True").LogTo(s => Debug.WriteLine(s));
        }
    }
}