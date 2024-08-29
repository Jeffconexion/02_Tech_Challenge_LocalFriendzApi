﻿using LocalFriendzApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LocalFriendzApi.Infrastructure.Data.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<AreaCode> AreasCode { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
