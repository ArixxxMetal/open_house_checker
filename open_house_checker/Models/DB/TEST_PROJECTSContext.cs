using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using open_house_checker.Models.StoredProcedures;
using open_house_checker.Models.ViewModels.Admin;
using open_house_checker.Models.ViewModels.Login;

namespace open_house_checker.Models.DB
{
    public partial class TEST_PROJECTSContext : DbContext
    {
        public TEST_PROJECTSContext()
        {
        }

        public TEST_PROJECTSContext(DbContextOptions<TEST_PROJECTSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmployeeTable> EmployeeTables { get; set; } = null!;
        public virtual DbSet<EmployeeVisitor> EmployeeVisitors { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public DbSet<SessionUserViewModel> GetLoginUserSP { get; set; }
        public DbSet<Sp_Return> CheckReturnSP { get; set; }
        public DbSet<get_total_count_ViewModel> GetTotalCountSP { get; set; }
        public DbSet<EmployeeVisitorViewModel> EmployeeVisitorQuery { get; set; }
        public DbSet<EmployeeReportViewModel> EmployeeReportQuery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SQL8002.site4now.net;Initial Catalog=db_a87bdd_fbc;User Id=db_a87bdd_fbc_admin;Password=Mit3ch2017!#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<EmployeeVisitorViewModel>();
            modelBuilder.Entity<EmployeeVisitorViewModel>();  //register stored procedure.

            modelBuilder.Ignore<EmployeeReportViewModel>();
            modelBuilder.Entity<EmployeeReportViewModel>();  //register stored procedure.

            modelBuilder.Ignore<SessionUserViewModel>();
            modelBuilder.Entity<SessionUserViewModel>();  //register stored procedure.

            modelBuilder.Ignore<get_total_count_ViewModel>();
            modelBuilder.Entity<get_total_count_ViewModel>();  //register stored procedure.

            modelBuilder.Ignore<Sp_Return>();
            modelBuilder.Entity<Sp_Return>();  //register stored procedure.

            modelBuilder.Entity<EmployeeTable>(entity =>
            {
                entity.ToTable("employee_table", "open_house");

                entity.Property(e => e.EmpArea)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("emp_area");

                entity.Property(e => e.EmpBum)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_bum");

                entity.Property(e => e.EmpCheckDate)
                    .HasColumnType("datetime")
                    .HasColumnName("emp_check_date");

                entity.Property(e => e.EmpDepId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_dep_id");

                entity.Property(e => e.EmpDepartment)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("emp_department");

                entity.Property(e => e.EmpIdRh)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("emp_id_rh");

                entity.Property(e => e.EmpIschecked).HasColumnName("emp_ischecked");

                entity.Property(e => e.EmpJob)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("emp_job");

                entity.Property(e => e.EmpJobId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emp_job_id");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("emp_name");
            });

            modelBuilder.Entity<EmployeeVisitor>(entity =>
            {
                entity.ToTable("employee_visitors", "open_house");

                entity.Property(e => e.VisitAge)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("visit_age");

                entity.Property(e => e.VisitEmpId).HasColumnName("visit_emp_id");

                entity.Property(e => e.VisitSex)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("visit_sex");

                entity.Property(e => e.VisitType)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("visit_type");

                entity.HasOne(d => d.VisitEmp)
                    .WithMany(p => p.EmployeeVisitors)
                    .HasForeignKey(d => d.VisitEmpId)
                    .HasConstraintName("FK__employee___visit__59FA5E80");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "open_house");

                entity.Property(e => e.UsrCreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("usr_create_date");

                entity.Property(e => e.UsrIdRh)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("usr_id_rh");

                entity.Property(e => e.UsrIsactive).HasColumnName("usr_isactive");

                entity.Property(e => e.UsrName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("usr_name");

                entity.Property(e => e.UsrPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("usr_password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
