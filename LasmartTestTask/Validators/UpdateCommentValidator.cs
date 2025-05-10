using FluentValidation;
using LasmartTestTask.Extensions;
using LasmartTestTask.ViewModels.Request;

namespace LasmartTestTask.Validators
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDto>
    {
        public UpdateCommentValidator()
        {
            RuleFor(c => c.Text).NotEmpty();
            RuleFor(c => c.ColorHEX).NotEmpty().HEX();
        }
    }
}
