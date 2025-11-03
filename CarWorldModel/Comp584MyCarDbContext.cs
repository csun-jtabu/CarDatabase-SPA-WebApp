using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarWorldModel;

public partial class Comp584MyCarDbContext : IdentityDbContext<CarWorldModelUser>
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
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").AddJsonFile("appsettings.Development.json");
        IConfigurationRoot config = builder.Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }

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
