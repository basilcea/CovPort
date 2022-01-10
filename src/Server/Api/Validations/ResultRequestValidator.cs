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
            RuleFor(x => x.BookingId).NotEmpty();
            RuleFor(x=> x.UserId).NotEmpty();
            RuleFor(x => x.TestType.ToUpper()).NotEmpty().IsEnumName(typeof(TestType));
        }
    }

    public class ResultPatchRequestValidator: AbstractValidator<ResultPatchRequestBody>
    {
         public ResultPatchRequestValidator()
        {
            RuleFor(x => x.Status.ToUpper()).NotEmpty().IsEnumName(typeof(TestStatus));
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => Boolean.Parse(x.Positive)).Equal(true);
        }
    }
}