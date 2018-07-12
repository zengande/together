﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Together.Activity.Infrastructure.Data;

namespace Together.Activity.Infrastructure.Migrations
{
    [DbContext(typeof(ActivityDbContext))]
    [Migration("20180430055910_EditActivityDateTimeColunms")]
    partial class EditActivityDateTimeColunms
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("Relational:Sequence:.activityseq", "'activityseq', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.participantseq", "'participantseq', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "activityseq")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<DateTime>("ActivitDate");

                    b.Property<int>("ActivityStatusId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<string>("Details");

                    b.Property<DateTime>("EndRegisterDate");

                    b.Property<DateTime>("EndTime");

                    b.Property<decimal?>("Funds");

                    b.Property<int?>("LimitsNum");

                    b.Property<int?>("OwnerId");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("ActivityStatusId");

                    b.ToTable("activities");
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.ActivityStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("activitystatus");
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "participantseq")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("ActivityId");

                    b.Property<string>("Avatar")
                        .HasMaxLength(200);

                    b.Property<DateTime>("JoinTime");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Sex")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("participant");
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Activity", b =>
                {
                    b.HasOne("Together.Activity.Domain.AggregatesModel.ActivityAggregate.ActivityStatus", "ActivityStatus")
                        .WithMany()
                        .HasForeignKey("ActivityStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Participant", b =>
                {
                    b.HasOne("Together.Activity.Domain.AggregatesModel.ActivityAggregate.Activity")
                        .WithMany("Participants")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
