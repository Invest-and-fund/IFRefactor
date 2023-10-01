﻿using AcmeStudios.ApiRefactor.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcmeStudios.ApiRefactor.DataAccess
{
    public class AcmeStudiosContext : DbContext
    {
        public DbSet<StudioItem> StudioItems => Set<StudioItem>();
        public DbSet<StudioItemType> StudioItemTypes => Set<StudioItemType>();
        public AcmeStudiosContext(DbContextOptions<AcmeStudiosContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudioItem>()
            .HasOne(s => s.StudioItemType)
            .WithMany(ad => ad.StudioItem)
            .HasForeignKey(ad => ad.StudioItemTypeId);

            modelBuilder.Entity<StudioItemType>().HasData(
                new StudioItemType { StudioItemTypeId = 1, Value = "Synthesiser" },
                new StudioItemType { StudioItemTypeId = 2, Value = "Drum Machine" },
                new StudioItemType { StudioItemTypeId = 3, Value = "Effect" },
                new StudioItemType { StudioItemTypeId = 4, Value = "Sequencer" },
                new StudioItemType { StudioItemTypeId = 5, Value = "Mixer" },
                new StudioItemType { StudioItemTypeId = 6, Value = "Oscillator" },
                new StudioItemType { StudioItemTypeId = 7, Value = "Utility" }
            );
        }
    }
}