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

    public class UpdateCommentsValidator : AbstractValidator<CreateCommentDto[]>
    {
        public UpdateCommentsValidator()
        {
            RuleForEach(c => c).SetValidator(new CreateCommentValidator());
        }
    }
}
