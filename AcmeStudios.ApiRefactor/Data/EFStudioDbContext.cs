using AcmeStudios.ApiRefactor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcmeStudios.ApiRefactor.Data
{
    public class EFStudioDbContext : DbContext
    {
        public EFStudioDbContext(DbContextOptions<EFStudioDbContext> options) : base(options)
        {
        }

        public DbSet<StudioItem> StudioItems { get; set; }
        public DbSet<StudioItemType> StudioItemTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudioItem>(ConfigureStudioItem);
            modelBuilder.Entity<StudioItemType>(ConfigureStudioItemType);

            //SeedStudioItemTypes(modelBuilder);
        }

        private void ConfigureStudioItem(EntityTypeBuilder<StudioItem> builder)
        {
            builder.HasOne(si => si.StudioItemType)
                .WithMany(st => st.StudioItem)
                .HasForeignKey(si => si.StudioItemTypeId);
        }

        private void ConfigureStudioItemType(EntityTypeBuilder<StudioItemType> builder)
        {
            builder.HasData(
                new StudioItemType { Id = 1, Value = "Synthesiser" },
                new StudioItemType { Id = 2, Value = "Drum Machine" },
                new StudioItemType { Id = 3, Value = "Effect" },
                new StudioItemType { Id = 4, Value = "Sequencer" },
                new StudioItemType { Id = 5, Value = "Mixer" },
                new StudioItemType { Id = 6, Value = "Oscillator" },
                new StudioItemType { Id = 7, Value = "Utility" }
            );
        }
        
    }
}
