﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartCooking.Data.Context;

namespace SmartCooking.Data.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("SmartCooking.Infastructure.Products.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ItemCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ItemCategoryId");

                    b.ToTable("SC_Item");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Products.ItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SC_ItemCategory");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Products.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SC_Unit");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Recipes.RecipeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Quantity")
                        .HasColumnType("REAL");

                    b.Property<int?>("RecipeHeaderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UnitId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UnitId");

                    b.ToTable("SC_RecipeDetail");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Recipes.RecipeHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("RecipeType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tags")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SC_RecipeHeader");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Recipes.RecipeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContentValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<long>("FileSize")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ProfileImage")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SC_RecipeImage");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Security.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FamilyName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SC_User");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Products.Item", b =>
                {
                    b.HasOne("SmartCooking.Infastructure.Products.ItemCategory", "ItemCategory")
                        .WithMany("Items")
                        .HasForeignKey("ItemCategoryId");

                    b.Navigation("ItemCategory");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Recipes.RecipeDetail", b =>
                {
                    b.HasOne("SmartCooking.Infastructure.Products.Item", "Item")
                        .WithMany("RecipeDetails")
                        .HasForeignKey("ItemId");

                    b.HasOne("SmartCooking.Infastructure.Products.Unit", "Unit")
                        .WithMany("RecipeDetails")
                        .HasForeignKey("UnitId");

                    b.Navigation("Item");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Products.Item", b =>
                {
                    b.Navigation("RecipeDetails");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Products.ItemCategory", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("SmartCooking.Infastructure.Products.Unit", b =>
                {
                    b.Navigation("RecipeDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
