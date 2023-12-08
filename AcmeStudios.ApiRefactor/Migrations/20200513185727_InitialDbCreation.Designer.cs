﻿// <auto-generated />
using System;
using AcemStudios.ApiRefactor.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcemStudios.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20200513185727_InitialDbCreation")]
    partial class InitialDbCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Api.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RpgClass")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Api.Models.StudioItem", b =>
                {
                    b.Property<int>("StudioItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Acquired")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eurorack")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Sold")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("SoldFor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StudioItemTypeId")
                        .HasColumnType("int");

                    b.HasKey("StudioItemId");

                    b.HasIndex("StudioItemTypeId");

                    b.ToTable("StudioItems");
                });

            modelBuilder.Entity("Api.Models.StudioItemImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("FileData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StudioItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudioItemId");

                    b.ToTable("StudioItemImages");
                });

            modelBuilder.Entity("Api.Models.StudioItemType", b =>
                {
                    b.Property<int>("StudioItemTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudioItemTypeId");

                    b.ToTable("StudioItemTypes");

                    b.HasData(
                        new
                        {
                            StudioItemTypeId = 1,
                            Value = "Synthesiser"
                        },
                        new
                        {
                            StudioItemTypeId = 2,
                            Value = "Drum Machine"
                        },
                        new
                        {
                            StudioItemTypeId = 3,
                            Value = "Effect"
                        },
                        new
                        {
                            StudioItemTypeId = 4,
                            Value = "Sequencer"
                        },
                        new
                        {
                            StudioItemTypeId = 5,
                            Value = "Mixer"
                        },
                        new
                        {
                            StudioItemTypeId = 6,
                            Value = "Oscillator"
                        },
                        new
                        {
                            StudioItemTypeId = 7,
                            Value = "Utility"
                        });
                });

            modelBuilder.Entity("Api.Models.StudioItem", b =>
                {
                    b.HasOne("Api.Models.StudioItemType", "StudioItemType")
                        .WithMany("StudioItem")
                        .HasForeignKey("StudioItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Models.StudioItemImage", b =>
                {
                    b.HasOne("Api.Models.StudioItem", "StudioItem")
                        .WithMany("StudioItemImage")
                        .HasForeignKey("StudioItemId");
                });
#pragma warning restore 612, 618
        }
    }
}
