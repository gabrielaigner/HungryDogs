﻿// <auto-generated />
using System;
using HungryDogs.Logic.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HungryDogs.Logic.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    partial class ProjectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HungryDogs.Logic.Entities.Persistence.OpeningHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<TimeSpan>("OpenFrom")
                        .HasColumnType("interval");

                    b.Property<TimeSpan>("OpenTo")
                        .HasColumnType("interval");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("Weekday")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("OpeningHour", "dbo");
                });

            modelBuilder.Entity("HungryDogs.Logic.Entities.Persistence.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("UniqueName")
                        .IsUnique();

                    b.ToTable("Restaurant", "dbo");
                });

            modelBuilder.Entity("HungryDogs.Logic.Entities.Persistence.SpecialOpeningHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("From")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("To")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("SpecialOpeningHour", "dbo");
                });

            modelBuilder.Entity("HungryDogs.Logic.Entities.Persistence.OpeningHour", b =>
                {
                    b.HasOne("HungryDogs.Logic.Entities.Persistence.Restaurant", "Restaurant")
                        .WithMany("OpeningHours")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("HungryDogs.Logic.Entities.Persistence.SpecialOpeningHour", b =>
                {
                    b.HasOne("HungryDogs.Logic.Entities.Persistence.Restaurant", "Restaurant")
                        .WithMany("SepcialOpeningHours")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("HungryDogs.Logic.Entities.Persistence.Restaurant", b =>
                {
                    b.Navigation("OpeningHours");

                    b.Navigation("SepcialOpeningHours");
                });
#pragma warning restore 612, 618
        }
    }
}
