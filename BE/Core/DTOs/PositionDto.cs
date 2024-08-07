namespace Core.DTOs
{
    public class PositionDto
    {
        public Guid PositionId { get; set; }
        /// <summary>
        /// Mã chức vụ
        /// </summary>
        public string PositionCode { get; set; } = null!;
        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName { get; set; } = null!;
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
    }
}
