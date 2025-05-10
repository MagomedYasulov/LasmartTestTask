using FluentValidation;
using LasmartTestTask.Extensions;
using LasmartTestTask.ViewModels.Request;

namespace LasmartTestTask.Validators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidator()
        {
            RuleFor(c => c.Text).NotEmpty();
            RuleFor(c => c.ColorHEX).NotEmpty().HEX();
        }
    }
}
