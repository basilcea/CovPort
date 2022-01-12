using Application.DTO;
using FluentValidation;

namespace Api.Validations
{
    public class SpaceRequestValidator : AbstractValidator<SpaceRequestBody>
    {
         public SpaceRequestValidator()
        {
            RuleFor(x => x.LocationName).NotEmpty();
            RuleFor(x => x.Date).NotEmpty().Matches(
                @"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))")
                .WithMessage("Date Format - 'YYYY-MM-DD', eg. 2022-02-12");
            RuleFor(x=> x.SpacesCreated).NotEmpty().GreaterThan(0);
        }
    }
}