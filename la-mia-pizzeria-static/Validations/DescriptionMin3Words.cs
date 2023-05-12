using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static
{
    public class DescriptionMin3Words : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var description = (string)value;
                var words = description.Split(' ');
                return words.Length >= 3;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Inserisci almeno 5 parole";
        }
    }
}