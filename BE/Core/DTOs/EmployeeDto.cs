using Core.Const;
using OfficeOpenXml.Attributes;
using OfficeOpenXml.Table;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    [EpplusTable(
        TableStyle = TableStyles.Dark1,
        PrintHeaders = true,
        AutoCalculate = false,
        AutofitColumns = true,
        ShowTotal = true,
        ShowFirstColumn = true)]
    public class EmployeeDto
    {
        [EpplusIgnore]
        public Guid EmployeeId { get; set; }
        [EpplusIgnore]
        public Guid? DepartmentId { get; set; }
        [EpplusIgnore]
        public Guid? PositionId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [EpplusTableColumn(Order = 1, Header = "Mã nhân viên")]
        [Required(ErrorMessage = EmployeeConst.ERROR_EMPLOYEECODE_EMPTY)]
        public string EmployeeCode { get; set; } = null!;
        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        [EpplusTableColumn(Order = 2, Header = "Họ và tên")]
        [Required(ErrorMessage = EmployeeConst.ERROR_FULLNAME_EMPTY)]
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [EpplusTableColumn(Order = 4, Header = "Ngày sinh", NumberFormat = "dd/MM/yyyy")]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính 0-Nam, 1-Nữ, 2-Khác
        /// </summary>
        [EpplusIgnore]
        public int? Gender { get; set; }
        /// <summary>
        /// Số CMTND/CCCD
        /// </summary>
        [EpplusTableColumn(Order = 6, Header = "CMTND/CCCD")]
        [Required(ErrorMessage = EmployeeConst.ERROR_IDENTITYNUMBER_EMPTY)]
        public string IdentityNumber { get; set; } = null!;
        /// <summary>
        /// Ngày cấp
        /// </summary>
        [EpplusIgnore]
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp
        /// </summary>
        [EpplusIgnore]
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        [EpplusTableColumn(Order = 8, Header = "Địa chỉ")]
        public string? Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [EpplusTableColumn(Order = 5, Header = "Số điện thoại")]
        [Required(ErrorMessage = EmployeeConst.ERROR_PHONENUMBER_EMPTY)]
        [Phone(ErrorMessage = EmployeeConst.ERROR_PHONENUMBER_FORMAT)]
        public string PhoneNumber { get; set; } = null!;
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [EpplusIgnore]
        public string? LandlineNumber { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        [EpplusTableColumn(Order = 3, Header = "Email")]
        [Required(ErrorMessage = EmployeeConst.ERROR_EMAIL_EMPTY)]
        [EmailAddress(ErrorMessage = EmployeeConst.ERROR_EMAIL_FORMAT)]
        public string Email { get; set; } = null!;
        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        [EpplusIgnore]
        public string? BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [EpplusIgnore]
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh
        /// </summary>
        [EpplusIgnore]
        public string? Branch { get; set; }
        /// <summary>
        /// Lương
        /// </summary>
        [EpplusIgnore]
        public decimal? Salary { get; set; }
        /// <summary>
        /// Tên giới tính
        /// </summary>
        [EpplusTableColumn(Order = 7, Header = "Giới tính")]
        public string? GenderName
        {
            get
            {
                return Gender switch
                {
                    (int)Enum.Gender.MALE => "Nam",
                    (int)Enum.Gender.FEMALE => "Nữ",
                    (int)Enum.Gender.OTHER => "Không xác định",
                    _ => "Không xác định",
                };
            }
            set { }
        }

    }
}
