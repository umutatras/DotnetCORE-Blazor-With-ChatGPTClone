﻿using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.Application.Common.Models.OpenAI;
using OpenAI.Interfaces;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using System.Globalization;
using ChatGPTClone.Domain.Enums;

namespace ChatGPTClone.Infrastructure.Services
{
    public class OpenAIManager : IOpenAiService
    {
        private readonly IOpenAIService _openAIService;
        private readonly ICurrentUserService _currentUserService;
        public OpenAIManager(IOpenAIService openAIService, ICurrentUserService currentUserService)
        {
            _openAIService = openAIService;
            _currentUserService = currentUserService;
        }

        public async Task<OpenAIChatResponse> ChatAsync(OpenAIChatRequest request, CancellationToken token)
        {
            var allMessages = GetChatMessages(request.Messages);

            var newMessage = ChatMessage.FromUser(request.Message);

            allMessages.Add(newMessage);

            var completionResult = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = allMessages,
                Model = GetChatGPTModel(request.Model),
                MaxTokens=4096
            });
            if (!completionResult.Successful)
            {
                throw new Exception(completionResult.Error.Message);
            }
            return new OpenAIChatResponse(completionResult.Choices.First().Message.Content);
        }
        private string GetChatGPTModel(GptModelType model)
        {
            return model switch
            {
                GptModelType.GPT4o => Models.Gpt_4o,
                GptModelType.GPT4oMini => Models.Gpt_4o_mini,
                GptModelType.GPT4 => Models.Gpt_4,
                _ => Models.Gpt_4o
            };
        }
        private List<ChatMessage> GetChatMessages(List<ChatGPTClone.Domain.ValueObjects.ChatMessage> messages)
        {
            return messages.Select(message => message.Type switch
            {
                ChatMessageType.System => ChatMessage.FromSystem(message.Content),
                ChatMessageType.Assistant => ChatMessage.FromAssistant(message.Content),
                _ => ChatMessage.FromUser(message.Content)
            }).ToList();
        }
    }
}
