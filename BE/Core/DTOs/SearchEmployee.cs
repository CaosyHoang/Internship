namespace Core.DTOs
{
    public class SearchEmployee
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; } = null!;
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Số CMTND, CCCD
        /// </summary>
        public string IdentityNumber { get; set; } = null!;
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; } = null!;
    }
}
