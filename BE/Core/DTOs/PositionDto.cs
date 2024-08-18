using Core.Const;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class PositionDto
    {
        public Guid PositionId { get; set; }
        /// <summary>
        /// Mã chức vụ
        /// </summary>
        [Required(ErrorMessage = PositionConst.ERROR_POSITIONCODE_EMPTY)]
        public string PositionCode { get; set; } = null!;
        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [Required(ErrorMessage = PositionConst.ERROR_POSITIONNAME_EMPTY)]
        public string PositionName { get; set; } = null!;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
