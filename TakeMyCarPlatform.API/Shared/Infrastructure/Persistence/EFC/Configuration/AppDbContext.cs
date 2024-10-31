﻿using TakeMyCarPlatform.API.CarManagement.Domain.Model.Aggregates;
using TakeMyCarPlatform.API.CarRental.Domain.Model.Aggregates;
using TakeMyCarPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace TakeMyCarPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteSource>().HasKey(f => f.Id);
            modelBuilder.Entity<FavoriteSource>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<FavoriteSource>().Property(f => f.SourceId).IsRequired();
            modelBuilder.Entity<FavoriteSource>().Property(f => f.NewsApiKey).IsRequired();

            modelBuilder.UseSnakeCaseNamingConvention();

        }
    }
}
