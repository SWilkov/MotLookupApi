using Microsoft.EntityFrameworkCore;
using MotLookupApi.DataLayer.MySQL.DataModels;

namespace MotLookupApi.DataLayer.MySQL.Data
{
  public class VehicleDataContext : DbContext
  {
    public DbSet<CommentDataModel> Comments { get; set; }
    public DbSet<MotTestDataModel> MotTests { get; set; }
    public DbSet<VehicleDataModel> Vehicles { get; set; }
    public DbSet<VehicleQueryDataModel> VehicleQueries { get; set; }
    public VehicleDataContext(DbContextOptions<VehicleDataContext> options): base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CommentDataModel>(entity =>
      {
        entity.HasKey(x => x.Id);
      });

      modelBuilder.Entity<MotTestDataModel>(entity =>
      {
        entity.HasKey(x =>x.Id);
        entity.HasMany(x => x.Comments)
              .WithOne(x => x.MotTest)
              .HasForeignKey(x => x.MotTestId)
              .OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<VehicleDataModel>(entity =>
      {
        entity.HasKey(x => x.Id);
        entity.HasMany(x => x.MotTests)
              .WithOne(x => x.Vehicle)
              .HasForeignKey(x => x.VehicleId)
              .OnDelete(DeleteBehavior.Cascade);
        entity.HasIndex(x => x.UniqueVehicleId);
        entity.Property(x => x.Registration).IsRequired();
      });

      modelBuilder.Entity<VehicleQueryDataModel>(entity =>
      {
        entity.HasKey(x => x.Id);
      });

      base.OnModelCreating(modelBuilder);
    }
  }
}
