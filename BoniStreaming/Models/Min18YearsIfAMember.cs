using System;
using System.ComponentModel.DataAnnotations;

namespace BoniStreaming.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthday == null)
            {
                return new ValidationResult("Birthday is required");
            }

            var age = DateTime.Today.Year - customer.Birthday.Value.Year;

            if (age < 18)
            {
                return new ValidationResult("You must be over 18 for payed memberships");
            }

            return ValidationResult.Success;
        }
    }
}