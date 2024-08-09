using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PKMGerejaEbenhaezer.Web.CustomValidations;

public class LessThanAttribute : ValidationAttribute, IClientModelValidator
{
    private const string _defaultErrorMessage = "{0} harus kurang dari {1}";

    private readonly string _otherPropertyName;
    private string? _otherPropertyDisplayName;

    public LessThanAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
    }

    public string GetErrorMessage() => ErrorMessage ?? _defaultErrorMessage;

    public override string FormatErrorMessage(string name)
    {
        return string.Format(GetErrorMessage(), name, _otherPropertyDisplayName ?? _otherPropertyName);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

        if (otherProperty is null) return ValidationResult.Success;

        if(otherProperty.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
            _otherPropertyDisplayName = displayAttribute.Name;

        var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

        if(otherPropertyValue is null) return ValidationResult.Success;

        if (value is not IComparable comparable)
            throw new Exception("Member type is not comparable");

        if (value.GetType() != otherPropertyValue.GetType())
            throw new Exception("Member and other property type is not equal");

        if(comparable.CompareTo(otherPropertyValue) >= 0)
        {
            var membersName = validationContext.MemberName is null
                ? null
                : new string[] { validationContext.MemberName, otherProperty.Name };

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), membersName);
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        context.Attributes.TryAdd("data-val", "true");
        context.Attributes.TryAdd("data-val-lessthan", _defaultErrorMessage ?? ErrorMessage);
        context.Attributes.TryAdd("data-val-lessthan-other", _otherPropertyName);
    }
}
