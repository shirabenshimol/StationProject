using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace stationProject.Dal.Models;

public partial class DbManager : DbContext
{
    public DbManager()
    {
    }

    public DbManager(DbContextOptions<DbManager> options)
        : base(options)
    {
    }

    public virtual DbSet<MeasurementsSummary> MeasurementsSummaries { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Downloads\\stationProject-20250312T193552Z-001-20250424T234139Z-001\\stationProject-20250312T193552Z-001\\stationProject\\Dal\\DataBaseנ\\DBsql.mdf;Integrated Security=True;Connect Timeout=30");
    //}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MeasurementsSummary>(entity =>
        {
            entity.HasKey(e => e.SummaryId).HasName("PK__Measurem__DAB10E2FED28493A");

            entity.ToTable("MeasurementsSummary");

            entity.HasOne(d => d.Station).WithMany(p => p.MeasurementsSummaries)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Station_Measurements");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Station__E0D8A6BD16D746C1");

            
            entity.ToTable("Station");
            entity.Property(e => e.StationId).ValueGeneratedOnAdd();

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.ManagerName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
