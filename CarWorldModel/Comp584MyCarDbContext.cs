using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarWorldModel;

public partial class Comp584MyCarDbContext : DbContext
{
    public Comp584MyCarDbContext()
    {
    }

    public Comp584MyCarDbContext(DbContextOptions<Comp584MyCarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CarMake> CarMakes { get; set; }

    public virtual DbSet<CarModel> CarModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\mssqllocaldb;initial catalog=COMP584-MyCarDBGolden");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.HasOne(d => d.Make).WithMany(p => p.CarModels)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarModels_CarMakes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
