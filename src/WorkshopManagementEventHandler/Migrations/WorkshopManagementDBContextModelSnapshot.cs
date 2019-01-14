﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pitstop.WorkshopManagementEventHandler.DataAccess;

namespace Pitstop.WorkshopManagementEventHandler.Migrations
{
    [DbContext(typeof(WorkshopManagementDBContext))]
    partial class WorkshopManagementDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Pitstop.WorkshopManagementEventHandler.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<string>("Telefono");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Pitstop.WorkshopManagementEventHandler.Model.MaintenanceJob", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ActualEndTime");

                    b.Property<DateTime?>("ActualStartTime");

                    b.Property<string>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Notes");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("VehicleMatricula");

                    b.Property<DateTime>("WorkshopPlanningDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VehicleMatricula");

                    b.ToTable("MaintenanceJob");
                });

            modelBuilder.Entity("Pitstop.WorkshopManagementEventHandler.Model.Vehicle", b =>
                {
                    b.Property<string>("Matricula")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Marca");

                    b.Property<string>("OwnerId");

                    b.Property<string>("Modelo");

                    b.HasKey("Matricula");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("Pitstop.WorkshopManagementEventHandler.Model.MaintenanceJob", b =>
                {
                    b.HasOne("Pitstop.WorkshopManagementEventHandler.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Pitstop.WorkshopManagementEventHandler.Model.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleMatricula");
                });
        }
    }
}
