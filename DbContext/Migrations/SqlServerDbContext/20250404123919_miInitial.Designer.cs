﻿// <auto-generated />
using System;
using DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    [DbContext(typeof(MainDbContext.SqlServerDbContext))]
    [Migration("20250404123919_miInitial")]
    partial class miInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ActivityLevelId");

                    b.ToTable("ActivityLevels", "supusr");

                    b.HasData(
                        new
                        {
                            ActivityLevelId = new Guid("aaebdf2e-b2ee-42a0-83b6-8283e043cc9a"),
                            Label = "Very Low Activity Level 🛌",
                            Name = "Very Low",
                            Rating = 1
                        },
                        new
                        {
                            ActivityLevelId = new Guid("f13ba987-3ae4-4b41-a277-4458039d1a54"),
                            Label = "Low Activity Level 🚶‍♂️",
                            Name = "Low",
                            Rating = 3
                        },
                        new
                        {
                            ActivityLevelId = new Guid("715feb05-f7ad-41a4-8c94-a7a388b0d11d"),
                            Label = "Medium Activity Level 🏃‍♂️",
                            Name = "Medium",
                            Rating = 5
                        },
                        new
                        {
                            ActivityLevelId = new Guid("e740e914-ddb0-4a29-a4d3-9823db77e8e7"),
                            Label = "High Activity Level 🏋️‍♂️",
                            Name = "High",
                            Rating = 7
                        },
                        new
                        {
                            ActivityLevelId = new Guid("43a8f861-ee03-4b36-9689-bcbbdcc350ab"),
                            Label = "Very High Activity Level 🏆",
                            Name = "Very High",
                            Rating = 10
                        });
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

                    b.Property<Guid>("PatientDbMPatientId")
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

                    b.HasData(
                        new
                        {
                            AppetiteLevelId = new Guid("931d839e-2818-4c92-abe0-6132eda005ec"),
                            Label = "Didn't Eat At All 🤢",
                            Name = "Didn't Eat At All",
                            Rating = 1
                        },
                        new
                        {
                            AppetiteLevelId = new Guid("1837abb8-e1b2-4f84-aa5e-c1d8129d4eb4"),
                            Label = "Little 🍽️",
                            Name = "Little",
                            Rating = 3
                        },
                        new
                        {
                            AppetiteLevelId = new Guid("fc5bd5de-12a4-4f74-9a5f-42f229602115"),
                            Label = "Normal Appetite 🙂",
                            Name = "Normal",
                            Rating = 5
                        },
                        new
                        {
                            AppetiteLevelId = new Guid("90095bbb-5853-48bc-a1fc-3750a4e65de7"),
                            Label = "Medium 😋",
                            Name = "Medium",
                            Rating = 7
                        },
                        new
                        {
                            AppetiteLevelId = new Guid("5afaad7e-bb1b-4b84-b9db-baab8c249a58"),
                            Label = "Very Much 🍴",
                            Name = "Very Much",
                            Rating = 10
                        });
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

                    b.Property<Guid>("PatientDbMPatientId")
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

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("MoodKindId");

                    b.ToTable("MoodKinds", "supusr");

                    b.HasData(
                        new
                        {
                            MoodKindId = new Guid("9ee6427e-b6a9-4a02-b3cf-96d54a5da45f"),
                            Label = "Very Low Mood Level 😞",
                            Name = "Very Low",
                            Rating = 1
                        },
                        new
                        {
                            MoodKindId = new Guid("16f3ffb6-b570-4fcd-afc3-32ef2ac7a40b"),
                            Label = "Low Mood Level 🙁",
                            Name = "Low",
                            Rating = 3
                        },
                        new
                        {
                            MoodKindId = new Guid("a0a10d06-9786-4b09-a3de-3303d7c02ee7"),
                            Label = "Medium Mood Level 😐",
                            Name = "Medium",
                            Rating = 5
                        },
                        new
                        {
                            MoodKindId = new Guid("e2350228-a342-4c45-9d70-b5527ae73b58"),
                            Label = "High Mood Level 🙂",
                            Name = "High",
                            Rating = 7
                        },
                        new
                        {
                            MoodKindId = new Guid("76a65e56-9608-4457-aee0-61c7bdf1b561"),
                            Label = "Very High Mood Level 😃",
                            Name = "Very High",
                            Rating = 10
                        });
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

                    b.HasData(
                        new
                        {
                            PatientId = new Guid("7851e798-4276-436e-959f-b73a75e74949"),
                            FirstName = "Madi",
                            LastName = "Alabama",
                            PersonalNumber = "19560831-1111"
                        },
                        new
                        {
                            PatientId = new Guid("dfacf606-3525-413e-8fdd-30bc853a4be4"),
                            FirstName = "John",
                            LastName = "Doe",
                            PersonalNumber = "19480516-2222"
                        },
                        new
                        {
                            PatientId = new Guid("db7cad5a-5110-4ef3-a7af-1ebde599504a"),
                            FirstName = "Jane",
                            LastName = "Smith",
                            PersonalNumber = "19610228-1212"
                        },
                        new
                        {
                            PatientId = new Guid("4da2d311-5d68-4afb-a51a-5ebc0c0fe69b"),
                            FirstName = "Alice",
                            LastName = "Johnson",
                            PersonalNumber = "19450801-4444"
                        },
                        new
                        {
                            PatientId = new Guid("6c7258bc-c685-4245-a3e1-e9ce072c628d"),
                            FirstName = "Bob",
                            LastName = "Brown",
                            PersonalNumber = "19501110-1331"
                        },
                        new
                        {
                            PatientId = new Guid("1be63c5b-2952-4437-93c1-d9ba2ee6aba8"),
                            FirstName = "Charlie",
                            LastName = "Davis",
                            PersonalNumber = "19511231-16181"
                        });
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

                    b.Property<Guid>("PatientDbMPatientId")
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

                    b.HasData(
                        new
                        {
                            SleepLevelId = new Guid("49768188-e86b-490c-9a5d-32bfd04a3626"),
                            Label = "Low Sleep Level 🙁",
                            Name = "Low",
                            Rating = 5
                        },
                        new
                        {
                            SleepLevelId = new Guid("4acf0c20-0fba-4209-803f-2acb33c4383b"),
                            Label = "Medium Sleep Level 😐",
                            Name = "Medium",
                            Rating = 6
                        },
                        new
                        {
                            SleepLevelId = new Guid("84a96e89-f9d7-4692-99a1-aac05decbdc9"),
                            Label = "OK Sleep Level 🙂",
                            Name = "OK",
                            Rating = 8
                        },
                        new
                        {
                            SleepLevelId = new Guid("836f52ae-fe07-46f3-814f-19bd91979c34"),
                            Label = "Too much Sleep Level 😃",
                            Name = "Too much",
                            Rating = 10
                        });
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

                    b.HasData(
                        new
                        {
                            StaffId = new Guid("b10a35c4-38c6-4da7-83e8-a1890db5d54a"),
                            FirstName = "Moris",
                            LastName = "Andre",
                            PersonalNumber = "19750105-1111"
                        },
                        new
                        {
                            StaffId = new Guid("f13f6843-3cf1-4a83-888a-505eb5cbd990"),
                            FirstName = "Madi",
                            LastName = "Alabama",
                            PersonalNumber = "19800613-1111"
                        },
                        new
                        {
                            StaffId = new Guid("a8156998-ff95-4856-bbef-5f24735e5425"),
                            FirstName = "Jane",
                            LastName = "Smith",
                            PersonalNumber = "19610228-1212"
                        },
                        new
                        {
                            StaffId = new Guid("a9f5e014-0708-4ca9-82f4-b7535956a28f"),
                            FirstName = "Alice",
                            LastName = "Johnson",
                            PersonalNumber = "19931001-4444"
                        },
                        new
                        {
                            StaffId = new Guid("6d8b4452-b136-460f-9305-56511cc5940c"),
                            FirstName = "John",
                            LastName = "Doe",
                            PersonalNumber = "19900516-2222"
                        });
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
                        .HasForeignKey("PatientDbMPatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                        .HasForeignKey("PatientDbMPatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                        .HasForeignKey("PatientDbMPatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
