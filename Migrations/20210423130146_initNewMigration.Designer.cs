﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Services;

namespace DiplomBackend.Migrations
{
    [DbContext(typeof(DbAppContext))]
    [Migration("20210423130146_initNewMigration")]
    partial class initNewMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Models.Appartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("DistrictValue")
                        .HasColumnType("real");

                    b.Property<float>("Floor")
                        .HasColumnType("real");

                    b.Property<int>("IdFromApi")
                        .HasColumnType("integer");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("RoomsCount")
                        .HasColumnType("real");

                    b.Property<float>("TotalSquare")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("apartment");
                });

            modelBuilder.Entity("Models.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("districts");
                });

            modelBuilder.Entity("Models.ImportantPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DistrictName")
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<int?>("TypePlaceId")
                        .HasColumnType("integer");

                    b.Property<string>("TypePlaceName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TypePlaceId");

                    b.ToTable("importantPlaces");
                });

            modelBuilder.Entity("Models.TypePlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("DistrictId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("typePlaces");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Models.ImportantPlace", b =>
                {
                    b.HasOne("Models.TypePlace", null)
                        .WithMany("ImportantPlace")
                        .HasForeignKey("TypePlaceId");
                });

            modelBuilder.Entity("Models.TypePlace", b =>
                {
                    b.HasOne("Models.District", null)
                        .WithMany("TypePlace")
                        .HasForeignKey("DistrictId");
                });

            modelBuilder.Entity("Models.District", b =>
                {
                    b.Navigation("TypePlace");
                });

            modelBuilder.Entity("Models.TypePlace", b =>
                {
                    b.Navigation("ImportantPlace");
                });
#pragma warning restore 612, 618
        }
    }
}
