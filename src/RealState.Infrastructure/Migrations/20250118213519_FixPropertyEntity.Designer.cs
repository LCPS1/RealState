﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealState.Infrastructure.Persistence;

#nullable disable

namespace RealState.Infrastructure.Migrations
{
    [DbContext(typeof(RealStateDbContext))]
    [Migration("20250118213519_FixPropertyEntity")]
    partial class FixPropertyEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RealState.Domain.Entities.Owner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PropertyOwners");
                });

            modelBuilder.Entity("RealState.Domain.Entities.PropertyBuilding", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeInternal")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("RealState.Domain.Entities.PropertyImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descrption")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImages");
                });

            modelBuilder.Entity("RealState.Domain.Entities.PropertyTrace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateSale")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Value")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyTraces");
                });

            modelBuilder.Entity("RealState.Domain.Entities.Owner", b =>
                {
                    b.OwnsOne("RealState.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("OwnerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.HasKey("OwnerId");

                            b1.ToTable("PropertyOwners");

                            b1.WithOwner()
                                .HasForeignKey("OwnerId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("RealState.Domain.Entities.PropertyBuilding", b =>
                {
                    b.HasOne("RealState.Domain.Entities.Owner", "Owner")
                        .WithMany("Properties")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("RealState.Domain.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PropertyBuildingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("nvarchar(255)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.HasKey("PropertyBuildingId");

                            b1.ToTable("Properties");

                            b1.WithOwner()
                                .HasForeignKey("PropertyBuildingId");
                        });

                    b.OwnsOne("RealState.Domain.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("PropertyBuildingId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)");

                            b1.HasKey("PropertyBuildingId");

                            b1.ToTable("Properties");

                            b1.WithOwner()
                                .HasForeignKey("PropertyBuildingId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("RealState.Domain.Entities.PropertyImage", b =>
                {
                    b.HasOne("RealState.Domain.Entities.PropertyBuilding", "Property")
                        .WithMany("Images")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("RealState.Domain.Entities.PropertyTrace", b =>
                {
                    b.HasOne("RealState.Domain.Entities.PropertyBuilding", "PropertyBuilding")
                        .WithMany("Traces")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("RealState.Domain.ValueObjects.Tax", "Tax", b1 =>
                        {
                            b1.Property<Guid>("PropertyTraceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Percentage")
                                .HasPrecision(18, 2)
                                .HasColumnType("decimal(18,2)");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)");

                            b1.HasKey("PropertyTraceId");

                            b1.ToTable("PropertyTraces");

                            b1.WithOwner()
                                .HasForeignKey("PropertyTraceId");
                        });

                    b.Navigation("PropertyBuilding");

                    b.Navigation("Tax")
                        .IsRequired();
                });

            modelBuilder.Entity("RealState.Domain.Entities.Owner", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RealState.Domain.Entities.PropertyBuilding", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Traces");
                });
#pragma warning restore 612, 618
        }
    }
}
