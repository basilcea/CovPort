using Application.DTO;
using FluentValidation;

namespace Api.Validations
{
    public class SpaceRequestValidator : AbstractValidator<SpaceRequestBody>
    {
         public SpaceRequestValidator()
        {
            RuleFor(x => x.LocationName).NotEmpty();
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}