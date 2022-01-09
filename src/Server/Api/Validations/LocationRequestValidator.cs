using Application.DTO;
using FluentValidation;

namespace Api.Validations
{
    public class LocationRequestValidator: AbstractValidator<LocationRequestBody>
    {
        public LocationRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
        
}