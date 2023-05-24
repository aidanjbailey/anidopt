﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Anidopt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Anidopt.Data
{
    public class AnidoptContext : IdentityDbContext
    {
        public AnidoptContext(DbContextOptions<AnidoptContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Species>()
                .HasIndex(at => at.Name)
                .IsUnique();
            builder.Entity<Organisation>()
                .HasIndex(o => o.Name)
                .IsUnique();
            builder.Entity<Breed>()
                .HasIndex(b => b.Name)
                .IsUnique();
            builder.Entity<DescriptorType>()
                .HasIndex(dt => dt.Name)
                .IsUnique();
        }

        public DbSet<Anidopt.Models.Animal> Animal { get; set; } = default!;

        public DbSet<Anidopt.Models.Species> Species { get; set; } = default!;

        public DbSet<Anidopt.Models.Organisation> Organisation { get; set; } = default!;

        public DbSet<Anidopt.Models.Breed> Breed { get; set; } = default!;

        public DbSet<Anidopt.Models.DescriptorType> DescriptorType { get; set; } = default!;

        public DbSet<Anidopt.Models.Descriptor> Descriptor { get; set; } = default!;

        public DbSet<Anidopt.Models.DescriptorLink> DescriptorLink { get; set; } = default!;

        public DbSet<Anidopt.Models.Sex> Sex { get; set; } = default!;

        public DbSet<Anidopt.Models.Estimation> Estimation { get; set; } = default!;

        public DbSet<Anidopt.Models.Picture> Picture { get; set; } = default!;

        public DbSet<Anidopt.Models.UserOrganisationLink> UserOrganisationLink { get; set; } = default!;

        public DbSet<Anidopt.Models.User> User { get; set; } = default!;
    }
}
