﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pitstop.CustomerManagementAPI.DataAccess;

namespace Pitstop.CustomerManagementAPI.Migrations
{
    [DbContext(typeof(CustomerManagementDBContext))]
    partial class CustomerManagementDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("Pitstop.CustomerManagementAPI.Model.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion");

                    b.Property<string>("Poblacion");

                    b.Property<string>("Email");

                    b.Property<string>("Nombre");

                    b.Property<string>("CodigoPostal");

                    b.Property<string>("Telefono");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });
        }
    }
}
