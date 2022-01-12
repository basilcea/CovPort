using System;
using Application.DTO;
using Domain.ValueObjects;
using FluentValidation;

namespace Api.Validations
{
    public class ResultPostRequestValidator: AbstractValidator<ResultPostRequestBody>
    {
        
         public ResultPostRequestValidator()
        {
            RuleFor(x => x.BookingId).NotEmpty().GreaterThan(0);
            RuleFor(x=> x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x=> x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.TestType.Trim().ToUpper()).NotEmpty().IsEnumName(typeof(TestType));
        }
    }

    public class ResultPatchRequestValidator: AbstractValidator<ResultPatchRequestBody>
    {
         public ResultPatchRequestValidator()
        {
            RuleFor(x => x.Status.Trim().ToUpper()).NotEmpty().IsEnumName(typeof(TestStatus));
            RuleFor(x => x.RequesterId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => Boolean.Parse(x.Positive)).Equal(true);
        }
    }
}