using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameInventoryManagement.Models
{
    public partial class gamemanagementContext : DbContext
    {
        public gamemanagementContext()
        {
        }

        public gamemanagementContext(DbContextOptions<gamemanagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<InventoryTable> InventoryTables { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Weapon> Weapons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root@123;database=gamemanagement", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<InventoryTable>(entity =>
            {
                entity.HasKey(e => e.InventoryId)
                    .HasName("PRIMARY");

                entity.ToTable("inventory_table");

                entity.HasIndex(e => e.UserId, "UserId_idx");

                entity.HasIndex(e => e.WeaponId, "WeaponId_idx");

                entity.Property(e => e.InventoryId).HasColumnName("inventoryId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.InventoryTables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserId");

                entity.HasOne(d => d.Weapon)
                    .WithMany(p => p.InventoryTables)
                    .HasForeignKey(d => d.WeaponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WeaponId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.Isadmin).HasColumnName("isadmin");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.ToTable("weapons");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
