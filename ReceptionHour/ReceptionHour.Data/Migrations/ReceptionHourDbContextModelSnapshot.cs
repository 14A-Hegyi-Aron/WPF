﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReceptionHour.Data;

#nullable disable

namespace ReceptionHour.Data.Migrations
{
    [DbContext(typeof(ReceptionHourDbContext))]
    partial class ReceptionHourDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ReceptionHour.Data.Models.MeetingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ParentName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("meetings", (string)null);
                });

            modelBuilder.Entity("ReceptionHour.Data.Models.TeacherModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Room")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("teachers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 15,
                            Name = "Teszt Elek",
                            Room = "501"
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 5,
                            Name = "Gipsz Jakab",
                            Room = "502"
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 10,
                            Name = "Winch Eszter",
                            Room = "503"
                        });
                });

            modelBuilder.Entity("ReceptionHour.Data.Models.MeetingModel", b =>
                {
                    b.HasOne("ReceptionHour.Data.Models.TeacherModel", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}