﻿// <auto-generated />
using System;
using ApartmentRentalWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApartmentRentalWebApi.Data.Migrations
{
    [DbContext(typeof(ApartmentRentalDbContext))]
    [Migration("20181229102201_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApartmentRentalWebApi.Domain.Entities.Apartment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<float>("Area")
                        .HasColumnName("area");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<bool>("IsRentable")
                        .HasColumnName("is_rentable");

                    b.Property<bool>("IsRented")
                        .HasColumnName("is_rented");

                    b.Property<double>("Latitude")
                        .HasColumnName("latitude");

                    b.Property<double>("Longitude")
                        .HasColumnName("longitude");

                    b.Property<short>("NrOfRooms")
                        .HasColumnName("nr_of_rooms");

                    b.Property<float>("PricePerMonth")
                        .HasColumnName("price_per_month");

                    b.Property<Guid>("RealtorId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RealtorId");

                    b.HasIndex("UserId");

                    b.ToTable("apartments");
                });

            modelBuilder.Entity("ApartmentRentalWebApi.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("ApartmentRentalWebApi.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email");

                    b.Property<string>("EmailConfirmationToken")
                        .IsRequired()
                        .HasColumnName("email_confirmation_token");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("email_confirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("ApartmentRentalWebApi.Domain.Entities.Apartment", b =>
                {
                    b.HasOne("ApartmentRentalWebApi.Domain.Entities.User", "Realtor")
                        .WithMany()
                        .HasForeignKey("RealtorId")
                        .HasConstraintName("fk_user_apartment_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApartmentRentalWebApi.Domain.Entities.User")
                        .WithMany("Apartments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ApartmentRentalWebApi.Domain.Entities.User", b =>
                {
                    b.HasOne("ApartmentRentalWebApi.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_user_role_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
