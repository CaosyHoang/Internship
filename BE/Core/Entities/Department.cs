using System;
using System.Collections.Generic;

namespace Infrastructure
{
    /// <summary>
    /// Danh sách phòng ban
    /// </summary>
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; } = null!;
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; } = null!;
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
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
