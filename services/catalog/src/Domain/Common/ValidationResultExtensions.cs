﻿using FluentValidation.Results;

namespace RecommendCoffee.Catalog.Domain.Common;

public static class ValidationResultExtensions
{
    public static IEnumerable<ValidationError> GetValidationErrors(this ValidationResult result)
    {
        return result.Errors
            .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
            .ToList();
    }
}