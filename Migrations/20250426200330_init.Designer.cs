﻿// <auto-generated />
using System;
using AymanProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AymanProject.Migrations
{
    [DbContext(typeof(EvaluationContext))]
    [Migration("20250426200330_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("AymanProject.Models.MainCriterian", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text_Ar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Text_En")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("MainCriterians");
                });

            modelBuilder.Entity("AymanProject.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SubmittedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("AymanProject.Models.ProjectMainCriterian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MainCriterianId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("UserEvaluation")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MainCriterianId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectMainCriterians");
                });

            modelBuilder.Entity("AymanProject.Models.ProjectSubCriterian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectMainCriterianId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubCriterianId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("UserEvaluation")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("ProjectMainCriterianId");

                    b.HasIndex("SubCriterianId");

                    b.ToTable("ProjectSubCriterians");
                });

            modelBuilder.Entity("AymanProject.Models.SubCriterian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MainId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text_Ar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Text_En")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("MainId");

                    b.ToTable("SubCriterians");
                });

            modelBuilder.Entity("AymanProject.Models.ProjectMainCriterian", b =>
                {
                    b.HasOne("AymanProject.Models.MainCriterian", "MainCriterian")
                        .WithMany()
                        .HasForeignKey("MainCriterianId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AymanProject.Models.Project", "Project")
                        .WithMany("ProjectMainCriterians")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainCriterian");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("AymanProject.Models.ProjectSubCriterian", b =>
                {
                    b.HasOne("AymanProject.Models.ProjectMainCriterian", "ProjectMainCriterian")
                        .WithMany("ProjectSubCriterians")
                        .HasForeignKey("ProjectMainCriterianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AymanProject.Models.SubCriterian", "SubCriterian")
                        .WithMany()
                        .HasForeignKey("SubCriterianId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProjectMainCriterian");

                    b.Navigation("SubCriterian");
                });

            modelBuilder.Entity("AymanProject.Models.SubCriterian", b =>
                {
                    b.HasOne("AymanProject.Models.MainCriterian", "MainCriterian")
                        .WithMany("SubCriterians")
                        .HasForeignKey("MainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainCriterian");
                });

            modelBuilder.Entity("AymanProject.Models.MainCriterian", b =>
                {
                    b.Navigation("SubCriterians");
                });

            modelBuilder.Entity("AymanProject.Models.Project", b =>
                {
                    b.Navigation("ProjectMainCriterians");
                });

            modelBuilder.Entity("AymanProject.Models.ProjectMainCriterian", b =>
                {
                    b.Navigation("ProjectSubCriterians");
                });
#pragma warning restore 612, 618
        }
    }
}
