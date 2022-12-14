// <auto-generated />
using System;
using AplicationDDD.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApplicationDDD.DAL.SqlLite.Migrations
{
    [DbContext(typeof(AppDB))]
    [Migration("20220910141129_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.Departamen", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("departamens");
                });

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.Employe", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronomic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salary")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("departamenid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("departamenid");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Orderid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Productid")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("Orderid");

                    b.HasIndex("Productid");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.Employe", b =>
                {
                    b.HasOne("AplicatioDDD.Domain.Entities.Departamen", "departamen")
                        .WithMany()
                        .HasForeignKey("departamenid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("departamen");
                });

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("AplicatioDDD.Domain.Entities.Order", null)
                        .WithMany("items")
                        .HasForeignKey("Orderid");

                    b.HasOne("AplicatioDDD.Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AplicatioDDD.Domain.Entities.Order", b =>
                {
                    b.Navigation("items");
                });
#pragma warning restore 612, 618
        }
    }
}
