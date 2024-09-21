using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Localization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTClone.Application.Features.ChatMessages.Commands
{
    public class ChatMessageCreateCommandValidator : AbstractValidator<ChatMessageCreateCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStringLocalizer<CommonLocalization> _localizer;
        public ChatMessageCreateCommandValidator(IApplicationDbContext context, IStringLocalizer<CommonLocalization> localizer)
        {
            _context = context;
            _localizer = localizer;

            RuleFor(x => x.ChatSessionId)
            .NotEmpty()
            .WithMessage(x => localizer[CommonLocalizationKeys.ValidationIsRequired, nameof(x.ChatSessionId)])
            .MustAsync(IsChatSessionExistsAsync)
            .WithMessage(x => localizer[CommonLocalizationKeys.ValidationIsRequired, nameof(x.ChatSessionId)]);

            RuleFor(x => x.Model)

                     .NotEmpty().WithMessage(x => localizer[CommonLocalizationKeys.ValidationIsRequired, nameof(x.Model)])
                     .IsInEnum().WithMessage(x => localizer[CommonLocalizationKeys.ValidationIsInvalid, nameof(x.Model)]);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage(x => localizer[CommonLocalizationKeys.ValidationIsRequired, nameof(x.Content)])
                .Length(5, 4000).WithMessage(x => localizer[CommonLocalizationKeys.ValidationMustBeBetween, nameof(x.Content), 5, 4000]);


            RuleFor(x => x)
            .MustAsync(IsChatThreadExistsAsync)
            .WithMessage(x => localizer[CommonLocalizationKeys.ValidationIsInvalid, nameof(x.ThreadId)]);
        }


        private Task<bool> IsChatThreadExistsAsync(ChatMessageCreateCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(command.ThreadId))
                return Task.FromResult(true);

            return _context.ChatSessions.AnyAsync(x => x.Id == command.ChatSessionId && x.Threads.Any(t => t.Id == command.ThreadId), cancellationToken);
        }



        private Task<bool> IsChatSessionExistsAsync(Guid chatSessionId, CancellationToken cancellationToken)
        {
            return _context.ChatSessions.AnyAsync(x => x.Id == chatSessionId, cancellationToken);
        }
    }
}