using System;
using System.Collections.Generic;

namespace Infrastructure
{
    /// <summary>
    /// Danh sách nhân viên
    /// </summary>
    public partial class Employee
    {
        public Guid EmployeeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; } = null!;
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateOnly? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính 0-Nam, 1-Nữ, 2-Khác
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// Số CMTND, CCCD
        /// </summary>
        public string IdentityNumber { get; set; } = null!;
        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateOnly? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? IndentityPlace { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? LandlineNumber { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? Branch { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Lương
        /// </summary>
        public decimal? Salary { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Position? Position { get; set; }
    }
}
