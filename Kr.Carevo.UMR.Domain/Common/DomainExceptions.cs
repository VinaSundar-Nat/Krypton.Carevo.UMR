using System;
using FluentValidation.Results;
using Kr.Common.Exceptions;

namespace Kr.Carevo.UMR.Domain.Common;

public static class DomainExceptions
{

    public static void ThrowDomainException(string title, (string property, string message) failures) =>
        throw new DomainValidationException(title, failures:
            [
                new(failures.property, failures.message)
            ]);

}