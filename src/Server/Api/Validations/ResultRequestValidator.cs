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
            RuleFor(x => x.TestType.Trim().ToUpper()).NotEmpty().IsEnumName(typeof(TestType)).WithMessage("Invalid TestType");
        }
    }

    public class ResultPatchRequestValidator: AbstractValidator<ResultPatchRequestBody>
    {
         public ResultPatchRequestValidator()
        {
            RuleFor(x => x.Status.Trim().ToUpper()).NotEmpty().IsEnumName(typeof(TestStatus)).WithMessage("Invalid Status");
            RuleFor(x => x.RequesterId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            bool val;
            RuleFor(x => Boolean.TryParse(x.Positive, out val)).Equal(true).WithMessage("Invalid Positive value");
        }
    }
}