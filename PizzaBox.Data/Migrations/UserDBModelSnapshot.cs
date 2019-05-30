﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaBox.Data;

namespace PizzaBox.Data.Migrations
{
    [DbContext(typeof(UserDB))]
    partial class UserDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PizzaBox.Data.Location", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Name");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("PizzaBox.Data.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Confirmed");

                    b.Property<decimal>("Cost");

                    b.Property<string>("CustomerEmail");

                    b.Property<string>("Location");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("CustomerEmail");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("PizzaBox.Data.Pizza", b =>
                {
                    b.Property<int>("PizzaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost");

                    b.Property<string>("Crust");

                    b.Property<int>("OrderId");

                    b.Property<string>("Size");

                    b.Property<string>("Toppings");

                    b.HasKey("PizzaId");

                    b.HasIndex("OrderId");

                    b.ToTable("Pizza");
                });

            modelBuilder.Entity("PizzaBox.Data.User", b =>
                {
                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Pass");

                    b.HasKey("Email");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PizzaBox.Data.Order", b =>
                {
                    b.HasOne("PizzaBox.Data.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerEmail");
                });

            modelBuilder.Entity("PizzaBox.Data.Pizza", b =>
                {
                    b.HasOne("PizzaBox.Data.Order")
                        .WithMany("Pizzas")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
