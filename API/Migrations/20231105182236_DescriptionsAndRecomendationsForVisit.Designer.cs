﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231105182236_DescriptionsAndRecomendationsForVisit")]
    partial class DescriptionsAndRecomendationsForVisit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.Clinic", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<Guid?>("ClinicOwnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DoctorId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsNFZ")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("NIP")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("OfficeHoursEndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("OfficeHoursStartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("WorkingDays")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.HasIndex("ClinicOwnerId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("API.Entities.ClinicDoctor", b =>
                {
                    b.Property<Guid>("ClinicId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uuid");

                    b.HasKey("ClinicId", "DoctorId");

                    b.HasIndex("DoctorId");

                    b.ToTable("ClinicDoctors");
                });

            modelBuilder.Entity("API.Entities.DoctorAdmissionConditions", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClinicId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("ConsultationFee")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("DoctorId")
                        .HasColumnType("uuid");

                    b.Property<bool?>("IsNFZ")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("Specialization")
                        .HasColumnType("text");

                    b.Property<DateTime?>("WorkHoursEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("WorkHoursStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int[]>("WorkingDays")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("DoctorAdmissionConditions");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("API.Entities.Visit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClinicId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("Fee")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Reccomendations")
                        .HasColumnType("text");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClinicId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("API.Entities.ClinicOwner", b =>
                {
                    b.HasBaseType("API.Entities.User");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("ClinicOwner");
                });

            modelBuilder.Entity("API.Entities.Doctor", b =>
                {
                    b.HasBaseType("API.Entities.User");

                    b.Property<string>("DoctorNumber")
                        .HasColumnType("text");

                    b.Property<bool>("IsClinicOwner")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uuid");

                    b.HasIndex("PhotoId");

                    b.ToTable("Users");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("API.Entities.Clinic", b =>
                {
                    b.HasOne("API.Entities.ClinicOwner", null)
                        .WithMany("Clinic")
                        .HasForeignKey("ClinicOwnerId");

                    b.HasOne("API.Entities.Doctor", null)
                        .WithMany("ClinicWork")
                        .HasForeignKey("DoctorId");
                });

            modelBuilder.Entity("API.Entities.ClinicDoctor", b =>
                {
                    b.HasOne("API.Entities.Clinic", "Clinic")
                        .WithMany("ClinicDoctors")
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Doctor", "Doctor")
                        .WithMany("ClinicDoctors")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("API.Entities.DoctorAdmissionConditions", b =>
                {
                    b.HasOne("API.Entities.Doctor", "Doctor")
                        .WithMany("AdmissionConditions")
                        .HasForeignKey("DoctorId");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("API.Entities.Visit", b =>
                {
                    b.HasOne("API.Entities.Clinic", "Clinic")
                        .WithMany()
                        .HasForeignKey("ClinicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.User", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clinic");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("API.Entities.Doctor", b =>
                {
                    b.HasOne("API.Entities.Photo", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("API.Entities.Clinic", b =>
                {
                    b.Navigation("ClinicDoctors");
                });

            modelBuilder.Entity("API.Entities.ClinicOwner", b =>
                {
                    b.Navigation("Clinic");
                });

            modelBuilder.Entity("API.Entities.Doctor", b =>
                {
                    b.Navigation("AdmissionConditions");

                    b.Navigation("ClinicDoctors");

                    b.Navigation("ClinicWork");
                });
#pragma warning restore 612, 618
        }
    }
}
