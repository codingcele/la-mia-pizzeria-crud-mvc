using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static
{
    public class IngredientsMin3Words : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var ingredients = (string)value;
                var words = ingredients.Split(' ');
                return words.Length >= 3;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} deve contenere almeno 3 parole.";
        }
    }
}
