using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project.Data.Models.Validation
{
    public class PasswordValidate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            const int MinLength = 6;
            const int MaxLength = 15;
            var password = Convert.ToString(value);

            bool meetsLengthRequirements = password.Length >= MinLength && password.Length <= MaxLength;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (meetsLengthRequirements)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = meetsLengthRequirements
                        && hasUpperCaseLetter
                        && hasLowerCaseLetter
                        && hasDecimalDigit;

            return isValid;
        }
    }
}
