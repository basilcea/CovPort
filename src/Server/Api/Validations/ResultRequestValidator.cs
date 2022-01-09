using System;
using Application.DTO;
using Domain.ValueObjects;
using FluentValidation;

namespace Api.Validations
{
    public class ResultPostRequestValidator: AbstractValidator<ResultPostRequestBody>
    {
         public ResultPostRequestValidator(bool value)
        {
            RuleFor(x => x.BookingId).NotEmpty();
            RuleFor(x=> x.UserId).NotEmpty();
            RuleFor(x => x.TestType.ToUpper()).NotEmpty().IsEnumName(typeof(TestType));
            RuleFor(x=> x.Status.ToUpper()).IsEnumName(typeof(TestStatus));
            RuleFor(x => Boolean.TryParse(x.Positive, out value)).Equal(true);
        }
    }

    public class ResultPatchRequestValidator: AbstractValidator<ResultPatchRequestBody>
    {
         public ResultPatchRequestValidator(bool value)
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x=> x.Status.ToUpper()).IsEnumName(typeof(TestStatus));
            RuleFor(x => Boolean.TryParse(x.Positive, out value)).Equal(true);
        }
    }
}