using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyWayNet.Models;

public partial class MyWayContext : DbContext
{
    public MyWayContext()
    {
    }

    public MyWayContext(DbContextOptions<MyWayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; } = null!;

    public virtual DbSet<Grade> Grades { get; set; } = null!;

    public virtual DbSet<Institution> Institutions { get; set; } = null!;

    public virtual DbSet<Occupation> Occupations { get; set; } = null!;

    public virtual DbSet<Record> Records { get; set; } = null!;

    public virtual DbSet<Scale> Scales { get; set; } = null!;

    public virtual DbSet<Skill> Skills { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    public virtual DbSet<ViewGradeScale> ViewGradeScales { get; set; } = null!;

    public virtual DbSet<ViewUserRecord> ViewUserRecords { get; set; } = null!;

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("event");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventBegin)
                .HasColumnType("datetime")
                .HasColumnName("event_begin");
            entity.Property(e => e.EventEnd)
                .HasColumnType("datetime")
                .HasColumnName("event_end");
            entity.Property(e => e.EventType).HasColumnName("event_type");
            entity.Property(e => e.InstitutionId).HasColumnName("institution_id");
            entity.Property(e => e.OccupationId).HasColumnName("occupation_id");

            entity.HasOne(d => d.Institution).WithMany(p => p.Events)
                .HasForeignKey(d => d.InstitutionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_event_institution");

            entity.HasOne(d => d.Occupation).WithMany(p => p.Events)
                .HasForeignKey(d => d.OccupationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_event_occupation");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.ToTable("grade");

            entity.Property(e => e.GradeId).HasColumnName("grade_id");
            entity.Property(e => e.GradeName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("grade_name");
            entity.Property(e => e.GradeValue).HasColumnName("grade_value");
            entity.Property(e => e.ScaleId).HasColumnName("scale_id");

            entity.HasOne(d => d.Scale).WithMany(p => p.Grades)
                .HasForeignKey(d => d.ScaleId)
                .HasConstraintName("FK_grade_scale");
        });

        modelBuilder.Entity<Institution>(entity =>
        {
            entity.ToTable("institution");

            entity.Property(e => e.InstitutionId).HasColumnName("institution_id");
            entity.Property(e => e.InstitutionAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("institution_address");
            entity.Property(e => e.InstitutionName)
                .HasMaxLength(20)
                .HasColumnName("institution_name");
            entity.Property(e => e.InstitutionType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("institution_type");
        });

        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.ToTable("occupation");

            entity.Property(e => e.OccupationId).HasColumnName("occupation_id");
            entity.Property(e => e.OccupationDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("occupation_description");
            entity.Property(e => e.OccupationName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("occupation_name");
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.ToTable("record");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.GradeId).HasColumnName("grade_id");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.Records)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_record_event");

            entity.HasOne(d => d.Grade).WithMany(p => p.Records)
                .HasForeignKey(d => d.GradeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_record_grade");

            entity.HasOne(d => d.Skill).WithMany(p => p.Records)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_record_skill");
        });

        modelBuilder.Entity<Scale>(entity =>
        {
            entity.ToTable("scale");

            entity.Property(e => e.ScaleId).HasColumnName("scale_id");
            entity.Property(e => e.ScaleMax).HasColumnName("scale_max");
            entity.Property(e => e.ScaleMin).HasColumnName("scale_min");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("skill");

            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.SkillDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("skill_description");
            entity.Property(e => e.SkillName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("skill_name");
            entity.Property(e => e.SkillType).HasColumnName("skill_type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_address");
            entity.Property(e => e.UserBirthdate)
                .HasColumnType("date")
                .HasColumnName("user_birthdate");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFirstname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_firstname");
            entity.Property(e => e.UserLastname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_lastname");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("user_password");
            entity.Property(e => e.UserRole)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('user')")
                .HasColumnName("user_role");
        });

        modelBuilder.Entity<ViewGradeScale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_GradeScales");

            entity.Property(e => e.GradeName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("grade_name");
            entity.Property(e => e.GradeValue).HasColumnName("grade_value");
            entity.Property(e => e.ScaleMax).HasColumnName("scale_max");
            entity.Property(e => e.ScaleMin).HasColumnName("scale_min");
        });

        modelBuilder.Entity<ViewUserRecord>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_UserRecords");

            entity.Property(e => e.EventBegin)
                .HasColumnType("datetime")
                .HasColumnName("event_begin");
            entity.Property(e => e.EventEnd)
                .HasColumnType("datetime")
                .HasColumnName("event_end");
            entity.Property(e => e.GradeName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("grade_name");
            entity.Property(e => e.GradeValue).HasColumnName("grade_value");
            entity.Property(e => e.InstitutionAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("institution_address");
            entity.Property(e => e.InstitutionName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("institution_name");
            entity.Property(e => e.InstitutionType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("institution_type");
            entity.Property(e => e.OccupationDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("occupation_description");
            entity.Property(e => e.OccupationName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("occupation_name");
            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.ScaleMax).HasColumnName("scale_max");
            entity.Property(e => e.ScaleMin).HasColumnName("scale_min");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFirstname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_firstname");
            entity.Property(e => e.UserLastname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_lastname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
