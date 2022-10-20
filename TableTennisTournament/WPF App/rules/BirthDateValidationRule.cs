using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TableTennisWPF.rules
{
    internal class BirthDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var date = (DateTime?)value;
                if (date == null)
                    return new ValidationResult(false, "Kötelező kitölteni!");
                if (date > DateTime.Today.AddYears(-5))
                    return new ValidationResult(false, "Nem lehet 5 évnél fiatalabb!");
                if (date < DateTime.Today.AddYears(-100))
                    return new ValidationResult(false, "Nem lehet 100 évnél idősebb!");

                return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Hibás dátum!");
            }
        }
    }
}
