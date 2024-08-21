﻿using ChatGPTClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTClone.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ChatSession> ChatSessions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();
    }
}
