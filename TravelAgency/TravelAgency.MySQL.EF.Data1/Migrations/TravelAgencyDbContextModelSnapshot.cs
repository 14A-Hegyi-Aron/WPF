﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgency.MySQL.EF.Data;

#nullable disable

namespace TravelAgency.MySQL.EF.Data.Migrations
{
    [DbContext(typeof(TravelAgencyDbContext))]
    partial class TravelAgencyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TravelAgency.Data.Models.ApplyModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("applies");
                });

            modelBuilder.Entity("TravelAgency.Data.Models.HotelModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("WebPageUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("hotels");
                });

            modelBuilder.Entity("TravelAgency.Data.Models.OfferModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<int>("ModeId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("longblob");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("ModeId");

                    b.ToTable("offers");
                });

            modelBuilder.Entity("TravelAgency.Data.Models.TravelModeModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("travelModes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "busz"
                        },
                        new
                        {
                            Id = 2,
                            Name = "repülő"
                        },
                        new
                        {
                            Id = 5,
                            Name = "hajó"
                        });
                });

            modelBuilder.Entity("TravelAgency.Data.Models.ApplyModel", b =>
                {
                    b.HasOne("TravelAgency.Data.Models.OfferModel", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("TravelAgency.Data.Models.OfferModel", b =>
                {
                    b.HasOne("TravelAgency.Data.Models.HotelModel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgency.Data.Models.TravelModeModel", "Mode")
                        .WithMany()
                        .HasForeignKey("ModeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("Mode");
                });
#pragma warning restore 612, 618
        }
    }
}
