using Application.DTO;
using FluentValidation;

namespace Api.Validations
{
    public class UserRequestValidator : AbstractValidator<UserRequestBody>
    {
         public UserRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        
        }

        
    }
}