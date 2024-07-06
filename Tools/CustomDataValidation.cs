using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tools;

public class CustomDataValidation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IntRangeValidation : ValidationAttribute
    {
        private readonly double _minValue;
        private readonly double _maxValue;

        public IntRangeValidation(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is double intValue)
            {
                if (intValue >= _minValue && intValue <= _maxValue)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"Value must be between {_minValue} and {_maxValue}");
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MinBidValidation : ValidationAttribute
    {
        private readonly double _minValue;
        

        public MinBidValidation(double minValue)
        {
            _minValue = minValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is double intValue)
            {
                if (intValue >= _minValue)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"Value must be larger {_minValue}");
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class AgeValidation : ValidationAttribute
    {
        private readonly int _minAge;
        private readonly int _maxAge;
        public AgeValidation(int minAge, int maxAge)
        {
            _minAge = minAge;
            _maxAge = maxAge;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dob)
            {
                int age = DateTime.Now.Year - dob.Year;
                if (dob.AddYears(age) > DateTime.Now.Date)
                {
                    age--;
                }
                if (age >= _minAge && age <= _maxAge)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"Age must be between {_minAge} and {_maxAge}");
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class MoneyValidation : ValidationAttribute
    {
        private readonly int _minAge;
        
        public MoneyValidation(int minAge)
        {
            _minAge = minAge;
            
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dob)
            {
                int age = DateTime.Now.Year - dob.Year;
                if (dob.AddYears(age) > DateTime.Now.Date)
                {
                    age--;
                }
                if (age >= _minAge )
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"Age must be higher {_minAge}");
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class EmailValidation : ValidationAttribute
    {
        public EmailValidation(){}

        public override bool IsValid(object? value)
        {
            if (value is string email)
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
                return Regex.IsMatch(email, pattern);
            }

            return false;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNumberValidation : ValidationAttribute
    {
        public PhoneNumberValidation() { }
        public override bool IsValid(object? value)
        {
            if (value is string phoneNumber)
            {
                string pattern = @"^[0-9]{10}$";
                return Regex.IsMatch(phoneNumber, pattern);
            }
            return false;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityValidation : ValidationAttribute
    {
        public IdentityValidation(){}

        public override bool IsValid(object? value)
        {
            if (value is string identity)
            {
                string pattern = @"^0(0[1-9]|[1-8][0-9]|9[0-6])[0-3]([0-9][0-9])[0-9]{6}$";
                return Regex.IsMatch(identity, pattern);
            }

            return false;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordValidation : ValidationAttribute
    {
        public PasswordValidation(){}

        public override bool IsValid(object? value)
        {
            if (value is string password)
            {
                string specialCharactersPattern = @".*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?].*";
                string uppercasePattern = @".*[A-Z].*";

                bool containsSpecialCharacters = Regex.IsMatch(password, specialCharactersPattern);
                bool containsUppercase = Regex.IsMatch(password, uppercasePattern);

                return containsSpecialCharacters && containsUppercase;
            }

            return false;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class EnumValidation : ValidationAttribute
    {
        private readonly Type _enumType;
        public EnumValidation(Type enumType)
        {
            _enumType = enumType;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && Enum.IsDefined(_enumType, value))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Invalid value for {validationContext.DisplayName}");
        }
    }
}