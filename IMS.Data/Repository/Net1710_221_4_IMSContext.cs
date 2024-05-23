﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IMS.Data.Models;

namespace IMS.Data.Repository;

public partial class Net1710_221_4_IMSContext : DbContext
{

    public Net1710_221_4_IMSContext()
    {
    }

    public Net1710_221_4_IMSContext(DbContextOptions<Net1710_221_4_IMSContext> options)
        : base(options)
    {

    }


    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Intern> Interns { get; set; }

    public virtual DbSet<InterviewsInfo> InterviewsInfos { get; set; }

    public virtual DbSet<Mentor> Mentors { get; set; }

    public virtual DbSet<Models.Task> Tasks { get; set; }

    public virtual DbSet<WorkingResult> WorkingResults { get; set; }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json", true, true)
                    .Build();
        var strConn = config.GetConnectionString("DefaultConnection");

        return strConn ?? throw new ArgumentNullException("Connection string is null");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString())
    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Intern>(entity =>
        {
            entity.HasKey(e => e.InternId).HasName("PK__Intern__CE265C53DCACDD51");

            entity.ToTable("Intern");

            entity.Property(e => e.InternId).HasColumnName("intern_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.EducationBackground).HasColumnName("education_background");
            entity.Property(e => e.Experiences).HasColumnName("experiences");
            entity.Property(e => e.JobPosition)
                .HasMaxLength(50)
                .HasColumnName("job_position");
            entity.Property(e => e.Major)
                .HasMaxLength(50)
                .HasColumnName("major");
            entity.Property(e => e.MentorId).HasColumnName("mentor_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.University)
                .HasMaxLength(100)
                .HasColumnName("university");
            entity.Property(e => e.WorkingTasks).HasColumnName("working_tasks");

            entity.HasOne(d => d.Company).WithMany(p => p.Interns)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Intern_Company");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Interns)
                .HasForeignKey(d => d.MentorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Intern_Mentor1");
        });

        modelBuilder.Entity<InterviewsInfo>(entity =>
        {
            entity.HasKey(e => e.InterviewinfoId).HasName("PK__intervie__E4A9940E7D71A669");

            entity.ToTable("interviews_info");

            entity.Property(e => e.InterviewinfoId).HasColumnName("interviewinfo_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.InternId).HasColumnName("intern_id");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Result)
                .HasMaxLength(20)
                .HasColumnName("result");
            entity.Property(e => e.Time)
                .HasColumnType("datetime")
                .HasColumnName("time");

            entity.HasOne(d => d.Intern).WithMany(p => p.InterviewsInfos)
                .HasForeignKey(d => d.InternId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_interviews_info_Intern2");
        });

        modelBuilder.Entity<Mentor>(entity =>
        {
            entity.HasKey(e => e.MentorId).HasName("PK__Mentor__E5D27EF313602FE1");

            entity.ToTable("Mentor");

            entity.Property(e => e.MentorId).HasColumnName("mentor_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.JobPosition)
                .HasMaxLength(100)
                .HasColumnName("job_position");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone");

            entity.HasOne(d => d.Company).WithMany(p => p.Mentors)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mentor_Company");
        });

        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.InternId).HasColumnName("intern_id");
            entity.Property(e => e.MentorId).HasColumnName("mentor_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Intern).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.InternId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Intern");

            entity.HasOne(d => d.Mentor).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.MentorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Mentor");
        });

        modelBuilder.Entity<WorkingResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__feedback__7A6B2B8C2ACE3141");

            entity.ToTable("WorkingResult");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .HasColumnName("created_by");
            entity.Property(e => e.InternId).HasColumnName("intern_id");
            entity.Property(e => e.MentorId).HasColumnName("mentor_id");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Intern).WithMany(p => p.WorkingResults)
                .HasForeignKey(d => d.InternId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkingResult_Intern");

            entity.HasOne(d => d.Mentor).WithMany(p => p.WorkingResults)
                .HasForeignKey(d => d.MentorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_feedback_Mentor");

            entity.HasOne(d => d.Task).WithMany(p => p.WorkingResults)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkingResult_Task");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
