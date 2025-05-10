using FluentValidation;
using LasmartTestTask.Extensions;
using LasmartTestTask.ViewModels.Request;

namespace LasmartTestTask.Validators
{
    public class UpdatePointValidator : AbstractValidator<UpdatePointDto>
    {
        public UpdatePointValidator()
        {
            RuleFor(p => p.X).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Y).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Radius).GreaterThan(0);
            RuleFor(p => p.ColorHEX).NotEmpty().HEX();
        }
    }
}
