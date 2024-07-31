using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public partial class HAUI_2021606204_CaoSyMinhHoangContext : DbContext
    {
        public HAUI_2021606204_CaoSyMinhHoangContext()
        {
        }

        public HAUI_2021606204_CaoSyMinhHoangContext(DbContextOptions<HAUI_2021606204_CaoSyMinhHoangContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=8.222.228.150;port=3306;database=HAUI_2021606204_CaoSyMinhHoang;uid=manhnv;pwd=12345678;sslmode=Preferred", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
                //Scaffold-DbContext "Server=8.222.228.150;Port=3306;Database=HAUI_2021606204_CaoSyMinhHoang;Uid=manhnv;Pwd=12345678;SslMode=Preferred;" Pomelo.EntityFrameworkCore.MySql -o ../Core/Entities
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.HasComment("Danh sách phòng ban")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DepartmentCode, "UK_Department_DepartmentCode")
                    .IsUnique();

                entity.HasIndex(e => e.DepartmentName, "UK_Department_DepartmentName")
                    .IsUnique();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(20)
                    .HasComment("Mã phòng ban");

                entity.Property(e => e.DepartmentName).HasComment("Tên phòng ban");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasComment("Mô tả");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasComment("Danh sách nhân viên")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DepartmentId, "IDX_Employee_DepartmentId");

                entity.HasIndex(e => e.PositionId, "IDX_Employee_PositionId");

                entity.HasIndex(e => e.Address, "UK_Employee_Address")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UK_Employee_Email")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeCode, "UK_Employee_EmployeeCode")
                    .IsUnique();

                entity.HasIndex(e => e.FullName, "UK_Employee_FullName")
                    .IsUnique();

                entity.HasIndex(e => e.IdentityNumber, "UK_Employee_IdentityNumber")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber, "UK_Employee_PhoneNumber")
                    .IsUnique();

                entity.Property(e => e.Address).HasComment("Địa chỉ");

                entity.Property(e => e.BankAccount)
                    .HasMaxLength(100)
                    .HasComment("Tài khoản ngân hàng");

                entity.Property(e => e.BankName)
                    .HasMaxLength(255)
                    .HasComment("Tên ngân hàng");

                entity.Property(e => e.Branch)
                    .HasMaxLength(255)
                    .HasComment("Chi nhánh");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.DateOfBirth).HasComment("Ngày sinh");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasComment("Địa chỉ email");

                entity.Property(e => e.EmployeeCode)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasComment("Mã nhân viên");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasComment("Họ tên nhân viên");

                entity.Property(e => e.Gender).HasComment("Giới tính 0-Nam, 1-Nữ, 2-Khác");

                entity.Property(e => e.IdentityDate).HasComment("Ngày cấp");

                entity.Property(e => e.IdentityNumber)
                    .HasMaxLength(25)
                    .HasComment("Số CMTND, CCCD");

                entity.Property(e => e.IndentityPlace)
                    .HasMaxLength(255)
                    .HasComment("Nơi cấp");

                entity.Property(e => e.LandlineNumber)
                    .HasMaxLength(50)
                    .HasComment("Số điện thoại cố định");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasComment("Số điện thoại");

                entity.Property(e => e.Salary)
                    .HasPrecision(18, 4)
                    .HasComment("Lương");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.HasComment("Danh sách chức vụ")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.PositionCode, "UK_Position_PositionCode")
                    .IsUnique();

                entity.HasIndex(e => e.PositionName, "UK_Position_PositionName")
                    .IsUnique();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasComment("Mô tả");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .HasComment("Người sửa");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày sửa");

                entity.Property(e => e.PositionCode)
                    .HasMaxLength(20)
                    .HasComment("Mã chức vụ");

                entity.Property(e => e.PositionName).HasComment("Tên chức vụ");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
