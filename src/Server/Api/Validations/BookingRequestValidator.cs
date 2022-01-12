using Application.DTO;
using Domain.ValueObjects;
using FluentValidation;

namespace Api.Validations
{
    public class BookingPostRequestValidator : AbstractValidator<BookingPostRequestBody>
    {
        public BookingPostRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.SpaceId).NotEmpty();
            RuleFor(x => x.TestType.ToUpper()).NotEmpty().IsEnumName(typeof(TestType));
        }
    }


    

     public class BookingPatchRequestValidator : AbstractValidator<BookingPatchRequestBody>
    {
        public BookingPatchRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x=> x.Status.Trim().ToUpper()).IsEnumName(typeof(BookingStatus));
        }

    }
}