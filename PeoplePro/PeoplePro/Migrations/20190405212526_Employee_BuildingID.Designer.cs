﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeoplePro.Models;

namespace PeoplePro.Migrations
{
    [DbContext(typeof(PeopleProContext))]
    [Migration("20190405212526_Employee_BuildingID")]
    partial class Employee_BuildingID
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PeoplePro.Models.Building", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("PeoplePro.Models.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentHQID");

                    b.Property<int?>("DepartmentHQID1");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentHQID1");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("PeoplePro.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuildingID");

                    b.Property<int>("DepartmentID");

                    b.Property<string>("FirstName")
                        .HasMaxLength(60);

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("PeoplePro.Models.Department", b =>
                {
                    b.HasOne("PeoplePro.Models.Building", "DepartmentHQ")
                        .WithMany()
                        .HasForeignKey("DepartmentHQID1");
                });

            modelBuilder.Entity("PeoplePro.Models.Employee", b =>
                {
                    b.HasOne("PeoplePro.Models.Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
