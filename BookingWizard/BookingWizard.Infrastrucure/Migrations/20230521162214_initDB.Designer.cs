﻿// <auto-generated />
using System;
using BookingWizard.Infrastrucure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingWizard.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230521162214_initDB")]
    partial class initDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingWizard.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("BookingWizard.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("arrival_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("date_of_departure")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("BookingWizard.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HotelLongDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelMark")
                        .HasColumnType("int");

                    b.Property<string>("HotelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("addressId")
                        .HasColumnType("int");

                    b.Property<string>("imageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isFavourite")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("addressId");

                    b.ToTable("hotels");
                });

            modelBuilder.Entity("BookingWizard.Models.hotelRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int?>("HotelId1")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("imageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isBooking")
                        .HasColumnType("bit");

                    b.Property<int>("roomPricePerNight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("HotelId1")
                        .IsUnique()
                        .HasFilter("[HotelId1] IS NOT NULL");

                    b.HasIndex("BookingId")
                        .IsUnique()
                        .HasFilter("[BookingId] IS NOT NULL");

                    b.ToTable("hotelRooms");
                });

            modelBuilder.Entity("BookingWizard.Models.Hotel", b =>
                {
                    b.HasOne("BookingWizard.Models.Address", "address")
                        .WithMany()
                        .HasForeignKey("addressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("address");
                });

            modelBuilder.Entity("BookingWizard.Models.hotelRoom", b =>
                {
                    b.HasOne("BookingWizard.Models.Hotel", "Hotel")
                        .WithMany("roomList")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingWizard.Models.Hotel", null)
                        .WithOne("Room")
                        .HasForeignKey("BookingWizard.Models.hotelRoom", "HotelId1");

                    b.HasOne("BookingWizard.Models.Booking", "Booking")
                        .WithOne("Room")
                        .HasForeignKey("BookingWizard.Models.hotelRoom", "BookingId");

                    b.Navigation("Hotel");

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("BookingWizard.Models.Booking", b =>
                {
                    b.Navigation("Room")
                        .IsRequired();
                });

            modelBuilder.Entity("BookingWizard.Models.Hotel", b =>
                {
                    b.Navigation("Room");

                    b.Navigation("roomList");
                });
#pragma warning restore 612, 618
        }
    }
}
