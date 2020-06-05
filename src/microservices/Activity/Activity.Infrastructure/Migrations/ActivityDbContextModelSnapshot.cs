﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.Migrations
{
    [DbContext(typeof(ActivityDbContext))]
    partial class ActivityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ActivityEndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ActivityStartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ActivityStatusId")
                        .HasColumnName("ActivityStatusId1")
                        .HasColumnType("int");

                    b.Property<int?>("AddressVisibleRuleId")
                        .HasColumnName("AddressVisibleRuleId1")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<DateTime>("EndRegisterTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LimitsNum")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("_activityStatusId")
                        .HasColumnName("ActivityStatusId")
                        .HasColumnType("int");

                    b.Property<int>("_addressVisibleRuleId")
                        .HasColumnName("AddressVisibleRuleId")
                        .HasColumnType("int");

                    b.Property<int>("_catalogId")
                        .HasColumnName("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActivityStatusId");

                    b.HasIndex("AddressVisibleRuleId");

                    b.HasIndex("_catalogId");

                    b.ToTable("AppActivities");
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.ActivityStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("AppActivityStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "RECRUITMENT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "PROCESSING"
                        },
                        new
                        {
                            Id = 3,
                            Name = "FINISHED"
                        },
                        new
                        {
                            Id = 4,
                            Name = "TIMEOUT"
                        },
                        new
                        {
                            Id = 5,
                            Name = "OBSOLETED"
                        });
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.AddressVisibleRule", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("AppAddressVisibleRules");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "PUBLICVISIBLE"
                        },
                        new
                        {
                            Id = 2,
                            Name = "PARTICIPANTSVISIBLE"
                        });
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Participant", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200);

                    b.Property<bool>("IsOwner")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("JoinTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int>("Sex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("UserId", "ActivityId");

                    b.HasIndex("ActivityId");

                    b.ToTable("AppParticipants");
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.CatalogAggregate.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.Property<int>("Order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int?>("ParentId")
                        .HasColumnName("ParentId1")
                        .HasColumnType("int");

                    b.Property<int?>("_parentId")
                        .HasColumnName("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("AppCatalogs");
                });

            modelBuilder.Entity("Together.BuildingBlocks.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("AppRequests");
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Activity", b =>
                {
                    b.HasOne("Together.Activity.Domain.AggregatesModel.ActivityAggregate.ActivityStatus", "ActivityStatus")
                        .WithMany()
                        .HasForeignKey("ActivityStatusId");

                    b.HasOne("Together.Activity.Domain.AggregatesModel.ActivityAggregate.AddressVisibleRule", "AddressVisibleRule")
                        .WithMany()
                        .HasForeignKey("AddressVisibleRuleId");

                    b.HasOne("Together.Activity.Domain.AggregatesModel.CatalogAggregate.Catalog", null)
                        .WithMany()
                        .HasForeignKey("_catalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ActivityId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnName("City")
                                .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                                .HasMaxLength(200)
                                .IsUnicode(true);

                            b1.Property<string>("DetailAddress")
                                .HasColumnName("DetailAddress")
                                .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
                                .HasMaxLength(512)
                                .IsUnicode(true);

                            b1.Property<double>("Latitude")
                                .ValueGeneratedOnAdd()
                                .HasColumnName("Latitude")
                                .HasColumnType("double")
                                .HasDefaultValue(0.0);

                            b1.Property<double>("Longitude")
                                .ValueGeneratedOnAdd()
                                .HasColumnName("Longitude")
                                .HasColumnType("double")
                                .HasDefaultValue(0.0);

                            b1.Property<string>("Province")
                                .HasColumnName("Province")
                                .HasColumnType("varchar(200) CHARACTER SET utf8mb4")
                                .HasMaxLength(200)
                                .IsUnicode(true);

                            b1.HasKey("ActivityId");

                            b1.ToTable("AppActivities");

                            b1.WithOwner()
                                .HasForeignKey("ActivityId");
                        });
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Participant", b =>
                {
                    b.HasOne("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Activity", null)
                        .WithMany("Participants")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.CatalogAggregate.Catalog", b =>
                {
                    b.HasOne("Together.Activity.Domain.AggregatesModel.CatalogAggregate.Catalog", null)
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });
#pragma warning restore 612, 618
        }
    }
}
