﻿// <auto-generated />
using GameInventoryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameInventoryManagement.Migrations
{
    [DbContext(typeof(gamemanagementContext))]
    partial class gamemanagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("GameInventoryManagement.Models.InventoryTable", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("inventoryId");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("InventoryId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UserId" }, "UserId_idx");

                    b.HasIndex(new[] { "WeaponId" }, "WeaponId_idx");

                    b.ToTable("inventory_table", (string)null);
                });

            modelBuilder.Entity("GameInventoryManagement.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("email");

                    b.Property<sbyte>("Isadmin")
                        .HasColumnType("tinyint")
                        .HasColumnName("isadmin");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("GameInventoryManagement.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.Property<int>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.ToTable("weapons", (string)null);
                });

            modelBuilder.Entity("GameInventoryManagement.Models.InventoryTable", b =>
                {
                    b.HasOne("GameInventoryManagement.Models.User", "User")
                        .WithMany("InventoryTables")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("UserId");

                    b.HasOne("GameInventoryManagement.Models.Weapon", "Weapon")
                        .WithMany("InventoryTables")
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("WeaponId");

                    b.Navigation("User");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("GameInventoryManagement.Models.User", b =>
                {
                    b.Navigation("InventoryTables");
                });

            modelBuilder.Entity("GameInventoryManagement.Models.Weapon", b =>
                {
                    b.Navigation("InventoryTables");
                });
#pragma warning restore 612, 618
        }
    }
}
