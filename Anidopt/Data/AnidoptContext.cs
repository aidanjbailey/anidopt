﻿using Microsoft.EntityFrameworkCore;
using Anidopt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Anidopt.Identity;

namespace Anidopt.Data {
    public class AnidoptContext : IdentityDbContext<AnidoptUser, AnidoptRole, int>
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
            #region AnidoptUser Constraints
            // Unique UserName
            builder.Entity<AnidoptUser>()
                .HasIndex(at => at.UserName)
                .IsUnique();
            #endregion
            #region DescriptorType Constraints
            // Name index
            builder.Entity<DescriptorType>()
                .HasIndex(dt => dt.Name)
                .IsUnique();
            // One-To-Many Descriptor
            builder.Entity<DescriptorType>()
                .HasMany(dt => dt.Descriptors)
                .WithOne(d => d.DescriptorType)
                .HasForeignKey(d => d.DescriptorTypeId)
                .HasPrincipalKey(dt => dt.Id);
            #endregion
            #region Descriptor Constraints
            // Name, DescriptorType index
            builder.Entity<Descriptor>()
                .HasIndex(d => new { d.Name, d.DescriptorTypeId })
                .IsUnique();
            // Many-To-One DescriptorType
            builder.Entity<Descriptor>()
                .HasOne(d => d.DescriptorType)
                .WithMany(dt => dt.Descriptors)
                .HasForeignKey(dt => dt.DescriptorTypeId)
                .HasPrincipalKey(d => d.Id);
            // One-To-Many DescriptorLink
            builder.Entity<Descriptor>()
                .HasMany(d => d.DescriptorLinks)
                .WithOne(dl => dl.Descriptor)
                .HasForeignKey(dl => dl.DescriptorId)
                .HasPrincipalKey(d => d.Id);
            #endregion
            #region DescriptorLink Constraints
            // AnimalId, DescriptorId index
            builder.Entity<DescriptorLink>()
                .HasIndex(dl => new { dl.AnimalId, dl.DescriptorId })
                .IsUnique();
            // Many-To-One Descriptor
            builder.Entity<DescriptorLink>()
                .HasOne(dl => dl.Descriptor)
                .WithMany(d => d.DescriptorLinks)
                .HasForeignKey(d => d.DescriptorId)
                .HasPrincipalKey(dl => dl.Id);
            // Many-To-One Animal
            builder.Entity<DescriptorLink>()
                .HasOne(dl => dl.Animal)
                .WithMany(a => a.DescriptorLinks)
                .HasForeignKey(a => a.AnimalId)
                .HasPrincipalKey(dl => dl.Id);
            #endregion
            #region Species Constraints
            // Name index
            builder.Entity<Species>()
                .HasIndex(s => s.Name)
                .IsUnique();
            // One-To-Many Breeds
            builder.Entity<Species>()
                .HasMany(s => s.Breeds)
                .WithOne(b => b.Species)
                .HasForeignKey(b => b.SpeciesId)
                .HasPrincipalKey(s => s.Id);
            #endregion
            #region AnimalColour Constraints
            // Colour index
            builder.Entity<AnimalColour>()
                .HasIndex(ac => ac.Colour)
                .IsUnique();
            // One-To-Many AnimalColourLinks
            builder.Entity<AnimalColour>()
                .HasMany(ac => ac.AnimalColourLinks)
                .WithOne(acl => acl.Colour)
                .HasForeignKey(acl => acl.ColourId)
                .HasPrincipalKey(ac => ac.Id);
            #endregion
            #region AnimalColourLink Constraints
            // AnimalId, ColourId index
            builder.Entity<AnimalColourLink>()
                .HasIndex(acl => new { acl.AnimalId, acl.ColourId })
                .IsUnique();
            // Many-To-One AnimalColours
            builder.Entity<AnimalColourLink>()
                .HasOne(acl => acl.Colour)
                .WithMany(c => c.AnimalColourLinks)
                .HasForeignKey(acl => acl.ColourId)
                .HasPrincipalKey(c => c.Id);
            // Many-To-One Animals
            builder.Entity<AnimalColourLink>()
                .HasOne(acl => acl.Animal)
                .WithMany(a => a.AnimalColourLinks)
                .HasForeignKey(a => a.ColourId)
                .HasPrincipalKey(acl => acl.Id);
            #endregion
            #region Picture Constraints
            // Name, AnimalId Index
            builder.Entity<Picture>()
                .HasIndex(p => new { p.Name, p.AnimalId })
                .IsUnique();
            // Many-To-One Animals
            builder.Entity<Picture>()
                .HasOne(p => p.Animal)
                .WithMany(a => a.Pictures)
                .HasForeignKey(a => a.AnimalId)
                .HasPrincipalKey(p => p.Id);
            #endregion
            #region Sex Constraints
            // Name Index
            builder.Entity<Sex>()
                .HasIndex(s => s.Name)
                .IsUnique();
            // One-To-Many Animals
            builder.Entity<Sex>()
                .HasMany(s => s.Animals)
                .WithOne(a => a.Sex)
                .HasForeignKey(a => a.SexId)
                .HasPrincipalKey(s => s.Id);
            #endregion
            #region Breed Constraints
            // Name, Species Index
            builder.Entity<Breed>()
                .HasIndex(b => new { b.Name, b.SpeciesId})
                .IsUnique();
            // Many-To-One Species
            builder.Entity<Breed>()
                .HasOne(b => b.Species)
                .WithMany(s => s.Breeds)
                .HasForeignKey(s => s.SpeciesId)
                .HasPrincipalKey(b => b.Id);
            // One-To-Many Animals
            builder.Entity<Breed>()
                .HasMany(b => b.Animals)
                .WithOne(a => a.Breed)
                .HasForeignKey(a => a.BreedId)
                .HasPrincipalKey(b => b.Id);
            #endregion
            #region Animal Constraints
            // Many-To-One Sex
            builder.Entity<Animal>()
                .HasOne(a => a.Sex)
                .WithMany(s => s.Animals)
                .HasForeignKey(a => a.SexId)
                .HasPrincipalKey(s => s.Id);
            // Many-To-One Breed
            builder.Entity<Animal>()
                .HasOne(a => a.Breed)
                .WithMany(b => b.Animals)
                .HasForeignKey(a => a.BreedId)
                .HasPrincipalKey(b => b.Id);
            // One-To-Many DescriptorLinks
            builder.Entity<Animal>()
                .HasMany(a => a.DescriptorLinks)
                .WithOne(dl => dl.Animal)
                .HasForeignKey(dl => dl.AnimalId)
                .HasPrincipalKey (a => a.Id);
            // One-To-Many AnimalColourLinks
            builder.Entity<Animal>()
                .HasMany(a => a.AnimalColourLinks)
                .WithOne(acl => acl.Animal)
                .HasForeignKey(acl => acl.AnimalId)
                .HasPrincipalKey(a => a.Id);
            // One-To-Many Pictures
            builder.Entity<Animal>()
                .HasMany(a => a.Pictures)
                .WithOne(p => p.Animal)
                .HasForeignKey(p => p.AnimalId)
                .HasPrincipalKey(a => a.Id);
            #endregion
        }

        public DbSet<Anidopt.Models.Size> Size { get; set; } = default!;

        public DbSet<Anidopt.Models.Animal> Animal { get; set; } = default!;

        public DbSet<Anidopt.Models.Species> Species { get; set; } = default!;

        public DbSet<Anidopt.Models.Breed> Breed { get; set; } = default!;

        public DbSet<Anidopt.Models.DescriptorType> DescriptorType { get; set; } = default!;

        public DbSet<Anidopt.Models.Descriptor> Descriptor { get; set; } = default!;

        public DbSet<Anidopt.Models.DescriptorLink> DescriptorLink { get; set; } = default!;

        public DbSet<Anidopt.Models.Sex> Sex { get; set; } = default!;

        public DbSet<Anidopt.Models.Picture> Picture { get; set; } = default!;

        public DbSet<AnidoptUser> AnidoptUser { get; set; } = default!;

        public DbSet<Anidopt.Models.AnimalColour> AnimalColour { get; set; } = default!;

        public DbSet<Anidopt.Models.AnimalColourLink> AnimalColourLink { get; set; } = default!;
    }
}
