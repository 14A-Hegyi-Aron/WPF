using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TableTennisWPF.rules
{
    internal class NumberValidationRule : ValidationRule
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = int.MaxValue;
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value.ToString(), out int number))
            {
                if (number < Min)
                {
                    return new ValidationResult(false, $"Túl kicsi szám: {Min}");
                }
                if (number > Max)
                {
                    return new ValidationResult(false, $"Túl nagy szám: {Max}");
                }
                return new ValidationResult(true, null);
            }
            else
            {
                {
                    return new ValidationResult(false, "Hibás szám");
                }
            }
        }
    }
}
