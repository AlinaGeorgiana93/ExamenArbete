﻿// <auto-generated />
using System;
using DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    [DbContext(typeof(MainDbContext.SqlServerDbContext))]
    partial class SqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbModels.ActivityDbM", b =>
                {
                    b.Property<Guid>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActivityLevelDbMActivityLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("PatientDbMPatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StrDate")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StrDayOfWeek")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ActivityId");

                    b.HasIndex("ActivityLevelDbMActivityLevelId");

                    b.HasIndex("PatientDbMPatientId");

                    b.ToTable("Activities", "supusr");
                });

            modelBuilder.Entity("DbModels.ActivityLevelDbM", b =>
                {
                    b.Property<Guid>("ActivityLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ActivityLevelId");

                    b.ToTable("ActivityLevels", "supusr");
                });

            modelBuilder.Entity("DbModels.AppetiteDbM", b =>
                {
                    b.Property<Guid>("AppetiteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AppetiteLevelDbMAppetiteLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("PatientDbMPatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StrDate")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StrDayOfWeek")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("AppetiteId");

                    b.HasIndex("AppetiteLevelDbMAppetiteLevelId");

                    b.HasIndex("PatientDbMPatientId");

                    b.ToTable("Appetites", "supusr");
                });

            modelBuilder.Entity("DbModels.AppetiteLevelDbM", b =>
                {
                    b.Property<Guid>("AppetiteLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("AppetiteLevelId");

                    b.ToTable("AppetiteLevels", "supusr");
                });

            modelBuilder.Entity("DbModels.GraphDbM", b =>
                {
                    b.Property<Guid>("GraphId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GraphId");

                    b.ToTable("Graphs", "supusr");
                });

            modelBuilder.Entity("DbModels.MoodDbM", b =>
                {
                    b.Property<Guid>("MoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<Guid?>("MoodKindDbMMoodKindId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("PatientDbMPatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StrDate")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StrDayOfWeek")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("MoodId");

                    b.HasIndex("MoodKindDbMMoodKindId");

                    b.HasIndex("PatientDbMPatientId");

                    b.ToTable("Moods", "supusr");
                });

            modelBuilder.Entity("DbModels.MoodKindDbM", b =>
                {
                    b.Property<Guid>("MoodKindId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("MoodKindId");

                    b.ToTable("MoodKinds", "supusr");
                });

            modelBuilder.Entity("DbModels.PatientDbM", b =>
                {
                    b.Property<Guid>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("GraphId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PersonalNumber")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("StaffDbMStaffId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PatientId");

                    b.HasIndex("GraphId")
                        .IsUnique()
                        .HasFilter("[GraphId] IS NOT NULL");

                    b.HasIndex("StaffDbMStaffId");

                    b.ToTable("Patients", "supusr");
                });

            modelBuilder.Entity("DbModels.SleepDbM", b =>
                {
                    b.Property<Guid>("SleepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("PatientDbMPatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SleepLevelDbMSleepLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StrDate")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StrDayOfWeek")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("SleepId");

                    b.HasIndex("PatientDbMPatientId");

                    b.HasIndex("SleepLevelDbMSleepLevelId");

                    b.ToTable("Sleeps", "supusr");
                });

            modelBuilder.Entity("DbModels.SleepLevelDbM", b =>
                {
                    b.Property<Guid>("SleepLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("SleepLevelId");

                    b.ToTable("SleepLevels", "supusr");
                });

            modelBuilder.Entity("DbModels.StaffDbM", b =>
                {
                    b.Property<Guid>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PersonalNumber")
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("StaffId");

                    b.ToTable("Staffs", "supusr");
                });

            modelBuilder.Entity("DbModels.UserDbM", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserId");

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("Models.DTO.GstUsrInfoDbDto", b =>
                {
                    b.Property<int>("NrActivities")
                        .HasColumnType("int");

                    b.Property<int>("NrAppetites")
                        .HasColumnType("int");

                    b.Property<int>("NrGraphs")
                        .HasColumnType("int");

                    b.Property<int>("NrMoods")
                        .HasColumnType("int");

                    b.Property<int>("NrPatients")
                        .HasColumnType("int");

                    b.Property<int>("NrSleeps")
                        .HasColumnType("int");

                    b.Property<int>("NrStaffs")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)");

                    b.ToTable((string)null);

                    b.ToView("vwInfoDb", "gstusr");
                });

            modelBuilder.Entity("Models.DTO.GstUsrInfoStaffsDto", b =>
                {
                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("NrPatients")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("vwInfoStaffs", "gstusr");
                });

            modelBuilder.Entity("DbModels.ActivityDbM", b =>
                {
                    b.HasOne("DbModels.ActivityLevelDbM", "ActivityLevelDbM")
                        .WithMany("ActivitiesDbM")
                        .HasForeignKey("ActivityLevelDbMActivityLevelId");

                    b.HasOne("DbModels.PatientDbM", "PatientDbM")
                        .WithMany("ActivitiesDbM")
                        .HasForeignKey("PatientDbMPatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityLevelDbM");

                    b.Navigation("PatientDbM");
                });

            modelBuilder.Entity("DbModels.AppetiteDbM", b =>
                {
                    b.HasOne("DbModels.AppetiteLevelDbM", "AppetiteLevelDbM")
                        .WithMany("AppetitesDbM")
                        .HasForeignKey("AppetiteLevelDbMAppetiteLevelId");

                    b.HasOne("DbModels.PatientDbM", "PatientDbM")
                        .WithMany("AppetitesDbM")
                        .HasForeignKey("PatientDbMPatientId");

                    b.Navigation("AppetiteLevelDbM");

                    b.Navigation("PatientDbM");
                });

            modelBuilder.Entity("DbModels.MoodDbM", b =>
                {
                    b.HasOne("DbModels.MoodKindDbM", "MoodKindDbM")
                        .WithMany("MoodsDbM")
                        .HasForeignKey("MoodKindDbMMoodKindId");

                    b.HasOne("DbModels.PatientDbM", "PatientDbM")
                        .WithMany("MoodsDbM")
                        .HasForeignKey("PatientDbMPatientId");

                    b.Navigation("MoodKindDbM");

                    b.Navigation("PatientDbM");
                });

            modelBuilder.Entity("DbModels.PatientDbM", b =>
                {
                    b.HasOne("DbModels.GraphDbM", "GraphDbM")
                        .WithOne("PatientDbM")
                        .HasForeignKey("DbModels.PatientDbM", "GraphId");

                    b.HasOne("DbModels.StaffDbM", null)
                        .WithMany("PatientsDbM")
                        .HasForeignKey("StaffDbMStaffId");

                    b.Navigation("GraphDbM");
                });

            modelBuilder.Entity("DbModels.SleepDbM", b =>
                {
                    b.HasOne("DbModels.PatientDbM", "PatientDbM")
                        .WithMany("SleepsDbM")
                        .HasForeignKey("PatientDbMPatientId");

                    b.HasOne("DbModels.SleepLevelDbM", "SleepLevelDbM")
                        .WithMany("SleepsDbM")
                        .HasForeignKey("SleepLevelDbMSleepLevelId");

                    b.Navigation("PatientDbM");

                    b.Navigation("SleepLevelDbM");
                });

            modelBuilder.Entity("DbModels.ActivityLevelDbM", b =>
                {
                    b.Navigation("ActivitiesDbM");
                });

            modelBuilder.Entity("DbModels.AppetiteLevelDbM", b =>
                {
                    b.Navigation("AppetitesDbM");
                });

            modelBuilder.Entity("DbModels.GraphDbM", b =>
                {
                    b.Navigation("PatientDbM")
                        .IsRequired();
                });

            modelBuilder.Entity("DbModels.MoodKindDbM", b =>
                {
                    b.Navigation("MoodsDbM");
                });

            modelBuilder.Entity("DbModels.PatientDbM", b =>
                {
                    b.Navigation("ActivitiesDbM");

                    b.Navigation("AppetitesDbM");

                    b.Navigation("MoodsDbM");

                    b.Navigation("SleepsDbM");
                });

            modelBuilder.Entity("DbModels.SleepLevelDbM", b =>
                {
                    b.Navigation("SleepsDbM");
                });

            modelBuilder.Entity("DbModels.StaffDbM", b =>
                {
                    b.Navigation("PatientsDbM");
                });
#pragma warning restore 612, 618
        }
    }
}
