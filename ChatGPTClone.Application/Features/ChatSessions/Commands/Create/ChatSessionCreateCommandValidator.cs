using FluentValidation;

namespace ChatGPTClone.Application.Features.ChatSessions.Commands.Create
{
    public class ChatSessionCreateCommandValidator : AbstractValidator<ChatSessionCreateCommand>
    {
        public ChatSessionCreateCommandValidator()
        {
            RuleFor(x=>x.Model).IsInEnum().WithMessage("Model is invalid").NotNull().WithMessage("Model is required").NotEmpty().WithMessage("Model is required");

            RuleFor(x => x.Content)
    .NotNull()
    .WithMessage("Content is required.")
    .NotEmpty()
    .WithMessage("Content is required.")
    .MaximumLength(4000)
    .WithMessage("Content must not exceed 4000 characters.");
        }
    }
}
