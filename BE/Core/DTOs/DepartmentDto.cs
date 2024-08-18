using Core.Const;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class DepartmentDto
    {
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Required(ErrorMessage = DepartmentConst.ERROR_DEPARTMENTCODE_EMPTY)]
        public string DepartmentCode { get; set; } = null!;
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required(ErrorMessage = DepartmentConst.ERROR_DEPARTMENTNAME_EMPTY)]
        public string DepartmentName { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
