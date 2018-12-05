using FluentValidation;
using Together.Activity.API.Applications.Commands;

namespace Together.Activity.API.Applications.Validations
{
    public class CreateActivityCommandValidator
        : AbstractValidator<CreateActivityCommand>
    {
        public CreateActivityCommandValidator()
        {

        }
    }
}
