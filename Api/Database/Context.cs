﻿using Microsoft.EntityFrameworkCore;
using Shared;

namespace Api;

public class Context : DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<GamePlatform> GamePlatforms { get; set; }
    public DbSet<GameTag> GameTags { get; set; }

    public Context(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<GamePlatform>()
            .HasOne(g => g.PlatformInstance)
            .WithMany(p => p.GamePlatforms)
            .OnDelete(DeleteBehavior.Cascade);
        mb.Entity<GameTag>()
            .HasOne(gt => gt.TagInstance)
            .WithMany(t => t.GameTags)
            .OnDelete(DeleteBehavior.Cascade);
        mb.Entity<Game>()
            .HasMany(g => g.GamePlatforms)
            .WithOne(gp => gp.Game)
            .OnDelete(DeleteBehavior.Cascade);


        mb.Entity<GamePlatform>()
            .HasKey(gp => new { gp.Platform, gp.GameId });
        mb.Entity<GameTag>()
            .HasKey(gt => new { gt.Tag, gt.GameId });

        base.OnModelCreating(mb);
    }

   
}