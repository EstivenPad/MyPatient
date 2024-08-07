﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPatient.DataAccess.DataContext;

#nullable disable

namespace MyPatient.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240721192149_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyPatient.Models.MA", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Exequatur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("MAs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Exequatur = "1536-23",
                            FirstName = "Miguel",
                            Identification = "402-1234567-0",
                            LastName = "Tejada",
                            Sex = false
                        });
                });

            modelBuilder.Entity("MyPatient.Models.MedicalOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Alergies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountDays")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("CreatedTime")
                        .HasColumnType("time");

                    b.Property<string>("Cures")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DREN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diagnostic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Enterconsult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneralMeasures")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Labs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MAId")
                        .HasColumnType("int");

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Room")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialControls")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MAId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalOrders");
                });

            modelBuilder.Entity("MyPatient.Models.MedicalOrderDetail", b =>
                {
                    b.Property<long>("MedicalOrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MedicalOrderDetailId"));

                    b.Property<string>("Dose")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Frecuency")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("MedicalOrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("SolutionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Via")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("MedicalOrderDetailId");

                    b.HasIndex("MedicalOrderId");

                    b.ToTable("MedicalOrderDetails");
                });

            modelBuilder.Entity("MyPatient.Models.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("ARS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<bool>("IsInsured")
                        .HasColumnType("bit");

                    b.Property<int>("MAId")
                        .HasColumnType("int");

                    b.Property<string>("NSS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Record")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MAId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ARS = "SeNaSa",
                            Age = 25,
                            Identification = "402-1234567-1",
                            IsInsured = true,
                            MAId = 1,
                            NSS = "1234",
                            Name = "Guillermo Reyes",
                            Record = "1234",
                            Sex = false,
                            Weight = 145.30000000000001
                        });
                });

            modelBuilder.Entity("MyPatient.Models.MedicalOrder", b =>
                {
                    b.HasOne("MyPatient.Models.MA", "MA")
                        .WithMany()
                        .HasForeignKey("MAId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyPatient.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MA");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MyPatient.Models.MedicalOrderDetail", b =>
                {
                    b.HasOne("MyPatient.Models.MedicalOrder", "MedicalOrder")
                        .WithMany("Solutions")
                        .HasForeignKey("MedicalOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalOrder");
                });

            modelBuilder.Entity("MyPatient.Models.Patient", b =>
                {
                    b.HasOne("MyPatient.Models.MA", "MA")
                        .WithMany()
                        .HasForeignKey("MAId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MA");
                });

            modelBuilder.Entity("MyPatient.Models.MedicalOrder", b =>
                {
                    b.Navigation("Solutions");
                });
#pragma warning restore 612, 618
        }
    }
}
