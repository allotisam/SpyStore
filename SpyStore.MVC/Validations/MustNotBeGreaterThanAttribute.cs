﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SpyStore.MVC.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class MustNotBeGreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        readonly string _prefix;
        readonly string _otherPropertyName;
        string _otherPropertyDisplayName;

        public MustNotBeGreaterThanAttribute(string otherPropertyName, string prefix = "") : this(otherPropertyName, "{0} must not be greater than {1}", prefix) { }

        public MustNotBeGreaterThanAttribute(string otherPropertyName, string errorMessage, string prefix) : base(errorMessage)
        {
            _otherPropertyName = otherPropertyName;
            _otherPropertyDisplayName = otherPropertyName;
            _prefix = prefix;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);
            SetOtherPropertyName(otherPropertyInfo);

            if (!int.TryParse(value.ToString(), out int toValidate))
            {
                return new ValidationResult($"{validationContext.DisplayName} must be numeric.");
            }

            var otherValue = (int)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            return toValidate > otherValue ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName)) : ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _otherPropertyDisplayName);
        }

        internal void SetOtherPropertyName(PropertyInfo otherPropertyInfo)
        {
            var displayAttribute = otherPropertyInfo.GetCustomAttributes<DisplayAttribute>().FirstOrDefault();
            _otherPropertyDisplayName = displayAttribute?.Name ?? _otherPropertyName;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            string propertyDisplayName = context.ModelMetadata.GetDisplayName();
            var propertyInfo = context.ModelMetadata.ContainerType.GetProperty(_otherPropertyName);
            SetOtherPropertyName(propertyInfo);
            string errorMessage = FormatErrorMessage(propertyDisplayName);

            context.Attributes.Add("data-val-notgreaterthan", errorMessage);
            context.Attributes.Add("data-val-notgreaterthan-otherpropertyname", _otherPropertyName);
            context.Attributes.Add("data-val-notgreaterthan-prefix", _prefix);
        }
    }
}
