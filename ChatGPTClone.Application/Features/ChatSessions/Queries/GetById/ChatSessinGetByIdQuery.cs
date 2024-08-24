﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetById
{
    public class ChatSessinGetByIdQuery:IRequest<ChatSessionGetByIdDto>
    {
        public Guid Id { get; set; }
        public ChatSessinGetByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
