﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelTracker.API.Data;

namespace TravelTracker.API.Migrations
{
    [DbContext(typeof(TravelTrackerDbContext))]
    partial class TravelTrackerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TravelTracker.API.Data.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acronym")
                        .IsRequired();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("TravelTracker.API.Data.DataModels.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FlightDepartureAirportId");

                    b.Property<int?>("FlightDestinationAirportId");

                    b.Property<int?>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("FlightDepartureAirportId");

                    b.HasIndex("FlightDestinationAirportId");

                    b.HasIndex("TripId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("TravelTracker.API.Data.DataModels.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("TravelTracker.API.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TravelTracker.API.Data.DataModels.Flight", b =>
                {
                    b.HasOne("TravelTracker.API.Data.Airport", "FlightDepartureAirport")
                        .WithMany()
                        .HasForeignKey("FlightDepartureAirportId");

                    b.HasOne("TravelTracker.API.Data.Airport", "FlightDestinationAirport")
                        .WithMany()
                        .HasForeignKey("FlightDestinationAirportId");

                    b.HasOne("TravelTracker.API.Data.DataModels.Trip")
                        .WithMany("TripFlights")
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("TravelTracker.API.Data.DataModels.Trip", b =>
                {
                    b.HasOne("TravelTracker.API.Data.User")
                        .WithMany("UserTrips")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
