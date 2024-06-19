using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dal.Dragones
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("La contraseña es obligatoria.");
            }

            var errors = new List<string>();

            // Verificar que la contraseña tenga al menos 8 caracteres
            if (password.Length < 8)
            {
                errors.Add("La contraseña debe tener al menos 8 caracteres.");
            }

            // Verificar que la contraseña contenga al menos una mayúscula, una minúscula, un número y un signo
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                errors.Add("La contraseña debe contener al menos una letra mayúscula.");
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                errors.Add("La contraseña debe contener al menos una letra minúscula.");
            }

            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                errors.Add("La contraseña debe contener al menos un número.");
            }

            if (!Regex.IsMatch(password, @"[\W_]"))
            {
                errors.Add("La contraseña debe contener al menos un signo especial.");
            }

            if (errors.Any())
            {
                return new ValidationResult(string.Join(Environment.NewLine, errors));
            }

            return ValidationResult.Success;
        }
    }
}
