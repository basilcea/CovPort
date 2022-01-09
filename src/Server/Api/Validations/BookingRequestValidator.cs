using Application.DTO;
using Domain.ValueObjects;
using FluentValidation;

namespace Api.Validations
{
    public class BookingRequestValidator : AbstractValidator<BookingRequestBody>
    {
         public BookingRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.SpaceId).NotEmpty();
            RuleFor(x => x.TestType).NotEmpty().IsEnumName(typeof(TestType));;
            RuleFor(x=> x.Status.ToUpper()).NotEmpty().IsEnumName(typeof(BookingStatus));
        }
    }
}