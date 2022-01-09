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
            RuleFor(x => x.TestType).NotEmpty().IsEnumName(typeof(TestType));
            RuleFor(x=> x.Status.ToUpper()).IsEnumName(typeof(BookingStatus));
        }

    }


     public class BookingPatchRequestValidator : AbstractValidator<BookingPatchRequestBody>
    {
        public BookingPatchRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x=> x.Status.ToUpper()).IsEnumName(typeof(BookingStatus));
        }

    }
}