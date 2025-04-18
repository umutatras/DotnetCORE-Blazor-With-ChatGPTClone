﻿using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.General;
using ChatGPTClone.Application.Common.Models.OpenAI;
using ChatGPTClone.Domain.Entities;
using ChatGPTClone.Domain.Enums;
using ChatGPTClone.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Features.ChatMessages.Commands
{
    public class ChatMessageCreateCommandHandler : IRequestHandler<ChatMessageCreateCommand, ResponseDto<ChatThread>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IOpenAiService _openAiService;
        private readonly ICurrentUserService _currentUserService;

        public ChatMessageCreateCommandHandler(IApplicationDbContext context, IOpenAiService openAiService, ICurrentUserService currentUserService)
        {
            _context = context;
            _openAiService = openAiService;
            _currentUserService = currentUserService;
        }

        public async Task<ResponseDto<ChatThread>> Handle(ChatMessageCreateCommand request, CancellationToken cancellationToken)
        {
            var chatSession = await _context
            .ChatSessions
            .FirstOrDefaultAsync(x => x.Id == request.ChatSessionId, cancellationToken);

            var oldMessages = chatSession
            .Threads
            .SelectMany(x => x.Messages).ToList();

            var userChatMessage = CreateChatMessage(request.Content, request.Model, ChatMessageType.User);

            var assistantChatMessage = await GetAssistantChatMessage(request.Model, request.Content, oldMessages, cancellationToken);

            var thread = AddMessagesToThread(chatSession, request.ThreadId, userChatMessage, assistantChatMessage);

            chatSession.ModifiedOn = DateTimeOffset.UtcNow;
            chatSession.ModifiedByUserId = _currentUserService.UserId.ToString();

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseDto<ChatThread>(thread, "Message was created successfully.");
        }

        private async Task<ChatMessage> GetAssistantChatMessage(GptModelType model, string userContent, List<ChatMessage> oldMessages, CancellationToken cancellationToken)
        {
            var response = await _openAiService.ChatAsync(new OpenAIChatRequest(model, userContent, oldMessages), cancellationToken);
            return CreateChatMessage(response.Response, model, ChatMessageType.Assistant);
        }

        private ChatThread AddMessagesToThread(ChatSession chatSession, string? threadId, ChatMessage userChatMessage, ChatMessage assistantChatMessage)
        {
            ChatThread thread;

            if (string.IsNullOrEmpty(threadId))
            {
                thread = new ChatThread
                {
                    Id = Ulid.NewUlid().ToString(),
                    Messages = new List<ChatMessage> { userChatMessage, assistantChatMessage },
                    CreatedOn = DateTimeOffset.UtcNow
                };
                chatSession.Threads.Add(thread);
            }
            else
            {
                thread = chatSession.Threads.FirstOrDefault(x => x.Id == threadId);

                thread.Messages.Add(userChatMessage);
                thread.Messages.Add(assistantChatMessage);
            }

            return thread;
        }

        private ChatMessage CreateChatMessage(string content, GptModelType model, ChatMessageType type)
        {
            return new ChatMessage
            {
                Id = Ulid.NewUlid().ToString(),
                Model = model,
                Type = type,
                Content = content,
                CreatedOn = DateTimeOffset.UtcNow
            };
        }
    }
}