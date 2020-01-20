using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StudentReg.sakila
{
    public partial class studentsregContext : DbContext
    {
        //public studentsregContext()
        //{
        //}

        public studentsregContext(DbContextOptions<studentsregContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Students> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseMySql("server=localhost;port=3306;user=professor;password=prof123;database=studentsreg", x => x.ServerVersion("8.0.18-mysql"));
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.HasKey(e => e.Idcourses)
                    .HasName("PRIMARY");

                entity.ToTable("courses");

                entity.Property(e => e.Idcourses)
                    .HasColumnName("idcourses")
                    .HasColumnType("int(10)");

                entity.Property(e => e.CourseName)
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

                entity.Property(e => e.CourseId)
                    .HasColumnName("courseId")
                    .HasColumnType("int(10)");

                entity.Property(e => e.StudentId)
                    .HasColumnName("studentId")
                    .HasColumnType("int(10)");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.IdStudents)
                    .HasName("PRIMARY");

                entity.ToTable("students");

                entity.Property(e => e.IdStudents)
                    .HasColumnName("idStudents")
                    .HasColumnType("int(10)");

                entity.Property(e => e.MiddleName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.SurName)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
