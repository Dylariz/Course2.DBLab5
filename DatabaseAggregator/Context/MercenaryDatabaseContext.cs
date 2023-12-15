using System;
using System.Collections.Generic;
using DatabaseAggregator.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DatabaseAggregator.Context;

public partial class MercenaryDatabaseContext : DbContext
{
    public MercenaryDatabaseContext() { }
    public MercenaryDatabaseContext(DbContextOptions<MercenaryDatabaseContext> options)
        : base(options) { }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<PersonalEquipment> PersonalEquipments { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Regiment> Regiments { get; set; }

    public virtual DbSet<RegimentEquipment> RegimentEquipments { get; set; }

    public virtual DbSet<Squad> Squads { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;database=mercenary;uid=root;pwd=123", ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("missions");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AwardUsd)
                .HasColumnType("int(11)")
                .HasColumnName("award_usd");
            entity.Property(e => e.Completed).HasColumnName("completed");
            entity.Property(e => e.Employer)
                .HasMaxLength(255)
                .HasColumnName("employer");
            entity.Property(e => e.Objective)
                .HasMaxLength(255)
                .HasColumnName("objective");
            entity.Property(e => e.Place)
                .HasMaxLength(255)
                .HasColumnName("place");
        });

        modelBuilder.Entity<PersonalEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("personal_equipment");

            entity.HasIndex(e => e.StaffId, "staff_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Count)
                .HasColumnType("int(11)")
                .HasColumnName("count");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.StaffId)
                .HasComment("null - в резерве	")
                .HasColumnType("int(11)")
                .HasColumnName("staff_id");

            entity.HasOne(d => d.Staff).WithMany(p => p.PersonalEquipments)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("personal_equipment_ibfk_1");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PRIMARY");

            entity.ToTable("position");

            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.IsOfficer).HasColumnName("is_officer");
        });

        modelBuilder.Entity<Regiment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("regiment");

            entity.HasIndex(e => e.CommanderId, "commander_id");

            entity.HasIndex(e => e.MissionId, "mission_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CommanderId)
                .HasColumnType("int(11)")
                .HasColumnName("commander_id");
            entity.Property(e => e.MissionId)
                .HasComment("null - нет задания")
                .HasColumnType("int(11)")
                .HasColumnName("mission_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Population)
                .HasColumnType("int(11)")
                .HasColumnName("population");
            entity.Property(e => e.SquadCount)
                .HasColumnType("int(11)")
                .HasColumnName("squad_count");

            entity.HasOne(d => d.Commander).WithMany(p => p.Regiments)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("regiment_ibfk_2");

            entity.HasOne(d => d.Mission).WithMany(p => p.Regiments)
                .HasForeignKey(d => d.MissionId)
                .HasConstraintName("regiment_ibfk_1");
        });

        modelBuilder.Entity<RegimentEquipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("regiment_equipment");

            entity.HasIndex(e => e.RegimentId, "division_id");

            entity.HasIndex(e => e.SquadId, "squad_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Count)
                .HasColumnType("int(11)")
                .HasColumnName("count");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.RegimentId)
                .HasComment("null - в резерве	")
                .HasColumnType("int(11)")
                .HasColumnName("regiment_id");
            entity.Property(e => e.SquadId)
                .HasComment("null - нет прикрепления к отдельной бригаде")
                .HasColumnType("int(11)")
                .HasColumnName("squad_id");

            entity.HasOne(d => d.Regiment).WithMany(p => p.RegimentEquipments)
                .HasForeignKey(d => d.RegimentId)
                .HasConstraintName("regiment_equipment_ibfk_1");

            entity.HasOne(d => d.Squad).WithMany(p => p.RegimentEquipments)
                .HasForeignKey(d => d.SquadId)
                .HasConstraintName("regiment_equipment_ibfk_2");
        });

        modelBuilder.Entity<Squad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("squad");

            entity.HasIndex(e => e.CommanderId, "commander_id");

            entity.HasIndex(e => e.RegimentId, "division_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CommanderId)
                .HasColumnType("int(11)")
                .HasColumnName("commander_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Population)
                .HasColumnType("int(11)")
                .HasColumnName("population");
            entity.Property(e => e.RegimentId)
                .HasColumnType("int(11)")
                .HasColumnName("regiment_id");

            entity.HasOne(d => d.Commander).WithMany(p => p.Squads)
                .HasForeignKey(d => d.CommanderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("squad_ibfk_2");

            entity.HasOne(d => d.Regiment).WithMany(p => p.Squads)
                .HasForeignKey(d => d.RegimentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("squad_ibfk_1");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("staff", tb => tb.HasComment("Служащие"));

            entity.HasIndex(e => e.RegimentId, "division_id");

            entity.HasIndex(e => e.Position, "position");

            entity.HasIndex(e => e.SquadId, "squad_id");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.EntryDate).HasColumnName("entry_date");
            entity.Property(e => e.InReserve)
                .HasComment("1 - в резерве")
                .HasColumnName("in_reserve");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.RegimentId)
                .HasComment("null - не прикреплён")
                .HasColumnType("int(11)")
                .HasColumnName("regiment_id");
            entity.Property(e => e.Salary)
                .HasColumnType("int(11)")
                .HasColumnName("salary");
            entity.Property(e => e.SquadId)
                .HasComment("null - не прикреплён")
                .HasColumnType("int(11)")
                .HasColumnName("squad_id");

            entity.HasOne(d => d.PositionNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.Position)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("staff_ibfk_3");

            entity.HasOne(d => d.Regiment).WithMany(p => p.Staff)
                .HasForeignKey(d => d.RegimentId)
                .HasConstraintName("staff_ibfk_1");

            entity.HasOne(d => d.Squad).WithMany(p => p.Staff)
                .HasForeignKey(d => d.SquadId)
                .HasConstraintName("staff_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
