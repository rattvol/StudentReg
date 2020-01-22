using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentReg.Models
{
    public partial class StudentsRegContext : DbContext
    {

        public StudentsRegContext(DbContextOptions<StudentsRegContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Students> Students { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.HasKey(e => e.CoursesID)
                    .HasName("PRIMARY");

                entity.ToTable("courses");

                entity.Property(e => e.CoursesID)
                    .HasColumnName("CoursesID")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CoursesName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("registration");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CoursesId)
                    .HasColumnName("CoursesId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.StudentsId)
                    .HasColumnName("StudentsId")
                    .HasColumnType("int(10)");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentsId)
                    .HasName("PRIMARY");

                entity.ToTable("students");

                entity.Property(e => e.StudentsId)
                    .HasColumnName("StudentsId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.StudentsMiddleName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StudentsName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.StudentsSurName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
