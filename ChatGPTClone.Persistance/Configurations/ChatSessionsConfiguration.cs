﻿using ChatGPTClone.Domain.Entities;
using ChatGPTClone.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ChatGPTClone.Persistance.Configurations;

public class ChatSessionConfiguration : IEntityTypeConfiguration<ChatSession>
{
    public void Configure(EntityTypeBuilder<ChatSession> builder)
    {
        // ID
        builder.HasKey(p => p.Id);

        // Title
        builder.Property(p => p.Title)
            .HasMaxLength(100)
            .IsRequired();

        // Model
        builder.Property(p => p.Model)
            .IsRequired()
            .HasConversion<int>();

        // Threads (JSONB configuration)
        builder.Property(p => p.Threads)
            .HasColumnType("jsonb")
            .IsRequired();

        // Configure JSON serialization options
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Threads => threads in JSON // ChatMessage => chatMessage
            WriteIndented = true
        };

        // Configure value conversion for Threads
        builder.Property(p => p.Threads)
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => JsonSerializer.Deserialize<List<ChatThread>>(v, jsonOptions) ?? new List<ChatThread>()
            );

        // Index on JSONB column for better performance
        builder.HasIndex(p => p.Threads)
            .HasMethod("gin");


        //// AppUser relationship
        //builder.HasOne(p => p.AppUser)
        //    .WithMany()
        //    .HasForeignKey(p => p.AppUserId)
        //    .OnDelete(DeleteBehavior.Cascade);

        // CreatedOn
        builder.Property(p => p.CreatedOn)
            .IsRequired();

        // CreatedByUserId
        builder.Property(p => p.CreatedByUserId)
            .IsRequired()
            .HasMaxLength(150);

        // ModifiedOn
        builder.Property(p => p.ModifiedOn)
            .IsRequired(false);

        // ModifiedByUserId
        builder.Property(p => p.ModifiedByUserId)
            .IsRequired(false)
            .HasMaxLength(150);

        builder.ToTable("ChatSessions");
    }
}