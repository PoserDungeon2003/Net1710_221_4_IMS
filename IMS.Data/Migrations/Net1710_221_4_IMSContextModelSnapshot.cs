﻿// <auto-generated />
using System;
using IMS.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IMS.Data.Migrations
{
    [DbContext(typeof(Net1710_221_4_IMSContext))]
    partial class Net1710_221_4_IMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IMS.Data.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("company_id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.HasKey("CompanyId");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("IMS.Data.Models.Intern", b =>
                {
                    b.Property<int>("InternId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("intern_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InternId"));

                    b.Property<string>("EducationBackground")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("education_background");

                    b.Property<string>("Experiences")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("experiences");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("job_position");

                    b.Property<string>("Major")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("major");

                    b.Property<int>("MentorId")
                        .HasColumnType("int")
                        .HasColumnName("mentor_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("university");

                    b.Property<string>("WorkingTasks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("working_tasks");

                    b.HasKey("InternId")
                        .HasName("PK__Intern__CE265C53DCACDD51");

                    b.HasIndex("MentorId");

                    b.ToTable("Intern", (string)null);
                });

            modelBuilder.Entity("IMS.Data.Models.InterviewsInfo", b =>
                {
                    b.Property<int>("InterviewinfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("interviewinfo_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InterviewinfoId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<int>("InternId")
                        .HasColumnType("int")
                        .HasColumnName("intern_id");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("result");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime")
                        .HasColumnName("time");

                    b.HasKey("InterviewinfoId")
                        .HasName("PK__intervie__E4A9940E7D71A669");

                    b.HasIndex("InternId");

                    b.ToTable("interviews_info", (string)null);
                });

            modelBuilder.Entity("IMS.Data.Models.Mentor", b =>
                {
                    b.Property<int>("MentorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("mentor_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MentorId"));

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("company_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("job_position");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("phone")
                        .IsFixedLength();

                    b.HasKey("MentorId")
                        .HasName("PK__Mentor__E5D27EF3779558B1");

                    b.HasIndex("CompanyId");

                    b.ToTable("Mentor", (string)null);
                });

            modelBuilder.Entity("IMS.Data.Models.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("task_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("description");

                    b.Property<int>("InternId")
                        .HasColumnType("int")
                        .HasColumnName("intern_id");

                    b.Property<int>("MentorId")
                        .HasColumnType("int")
                        .HasColumnName("mentor_id");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("TaskId");

                    b.HasIndex("InternId");

                    b.HasIndex("MentorId");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("IMS.Data.Models.WorkingResult", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("result_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultId"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("created_by");

                    b.Property<int>("InternId")
                        .HasColumnType("int")
                        .HasColumnName("intern_id");

                    b.Property<int>("MentorId")
                        .HasColumnType("int")
                        .HasColumnName("mentor_id");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("note");

                    b.Property<double?>("Rating")
                        .HasColumnType("float")
                        .HasColumnName("rating");

                    b.Property<int>("TaskId")
                        .HasColumnType("int")
                        .HasColumnName("task_id");

                    b.HasKey("ResultId")
                        .HasName("PK__feedback__7A6B2B8C2ACE3141");

                    b.HasIndex("InternId");

                    b.HasIndex("MentorId");

                    b.HasIndex("TaskId");

                    b.ToTable("WorkingResult", (string)null);
                });

            modelBuilder.Entity("IMS.Data.Models.Company", b =>
                {
                    b.HasOne("IMS.Data.Models.Intern", "CompanyNavigation")
                        .WithOne("Company")
                        .HasForeignKey("IMS.Data.Models.Company", "CompanyId")
                        .IsRequired()
                        .HasConstraintName("FK_Company_Intern");

                    b.Navigation("CompanyNavigation");
                });

            modelBuilder.Entity("IMS.Data.Models.Intern", b =>
                {
                    b.HasOne("IMS.Data.Models.Mentor", "Mentor")
                        .WithMany("Interns")
                        .HasForeignKey("MentorId")
                        .IsRequired()
                        .HasConstraintName("FK_Intern_Mentor1");

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("IMS.Data.Models.InterviewsInfo", b =>
                {
                    b.HasOne("IMS.Data.Models.Intern", "Intern")
                        .WithMany("InterviewsInfos")
                        .HasForeignKey("InternId")
                        .IsRequired()
                        .HasConstraintName("FK_interviews_info_Intern2");

                    b.Navigation("Intern");
                });

            modelBuilder.Entity("IMS.Data.Models.Mentor", b =>
                {
                    b.HasOne("IMS.Data.Models.Company", "Company")
                        .WithMany("Mentors")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_Mentor_Company");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("IMS.Data.Models.Task", b =>
                {
                    b.HasOne("IMS.Data.Models.Intern", "Intern")
                        .WithMany("Tasks")
                        .HasForeignKey("InternId")
                        .IsRequired()
                        .HasConstraintName("FK_Task_Intern");

                    b.HasOne("IMS.Data.Models.Mentor", "Mentor")
                        .WithMany("Tasks")
                        .HasForeignKey("MentorId")
                        .IsRequired()
                        .HasConstraintName("FK_Task_Mentor");

                    b.Navigation("Intern");

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("IMS.Data.Models.WorkingResult", b =>
                {
                    b.HasOne("IMS.Data.Models.Intern", "Intern")
                        .WithMany("WorkingResults")
                        .HasForeignKey("InternId")
                        .IsRequired()
                        .HasConstraintName("FK_WorkingResult_Intern");

                    b.HasOne("IMS.Data.Models.Mentor", "Mentor")
                        .WithMany("WorkingResults")
                        .HasForeignKey("MentorId")
                        .IsRequired()
                        .HasConstraintName("FK_feedback_Mentor");

                    b.HasOne("IMS.Data.Models.Task", "Task")
                        .WithMany("WorkingResults")
                        .HasForeignKey("TaskId")
                        .IsRequired()
                        .HasConstraintName("FK_WorkingResult_Task");

                    b.Navigation("Intern");

                    b.Navigation("Mentor");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("IMS.Data.Models.Company", b =>
                {
                    b.Navigation("Mentors");
                });

            modelBuilder.Entity("IMS.Data.Models.Intern", b =>
                {
                    b.Navigation("Company");

                    b.Navigation("InterviewsInfos");

                    b.Navigation("Tasks");

                    b.Navigation("WorkingResults");
                });

            modelBuilder.Entity("IMS.Data.Models.Mentor", b =>
                {
                    b.Navigation("Interns");

                    b.Navigation("Tasks");

                    b.Navigation("WorkingResults");
                });

            modelBuilder.Entity("IMS.Data.Models.Task", b =>
                {
                    b.Navigation("WorkingResults");
                });
#pragma warning restore 612, 618
        }
    }
}
