using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Net;

namespace Core.Services
{
    internal sealed class EmployeeService : BaseService<Employee>, IEmployeeService
    {

        #region Declaration

        private readonly IRepositoryManager _repoManager;
        private readonly ILoggerManager _logger;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public EmployeeService(IRepositoryManager repoManager, ILoggerManager logger, IMapper mapper) : base(repoManager.Employee, mapper)
        {
            _repoManager = repoManager;
            _logger = logger;
        }

        public async Task<ResultDetails> ExportEmployeeAsync()
        {
            // Lấy danh sách nhân viên:
            var employees = await _repoManager.Employee.GetAsync();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");
                worksheet.Cells["A1"].LoadFromCollection(employees, true, OfficeOpenXml.Table.TableStyles.Medium1);

                // Mã hóa file Excel thành Base64
                var fileBytes = package.GetAsByteArray();
                var base64String = Convert.ToBase64String(fileBytes);

                return new ResultDetails
                {
                    Success = true,
                    Data = new
                    {
                        Data = base64String,
                        FileName = "employees.xlsx",
                    },
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
        }

        public async Task<ResultDetails> ImportEmployeeAsync(IFormFile file)
        {
            // Kiểm tra file:
            Filter.CheckFileImport(file);
            var count = 0;
            var employees = new List<Employee>();
            using (var stream = new MemoryStream())
            {
                // Copy tệp vào Stream:
                await file.CopyToAsync(stream);
                // Thực hiện đọc dữ liệu:
                using (var package = new ExcelPackage(stream))
                {
                    // Sheet đọc dữ liệu:
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    if (worksheet != null)
                    {
                        // Tổng số dòng dữ liệu:
                        var rowCount = worksheet.Dimension.Rows;
                        // Bắt đầu đọc dữ liệu (từ dòng thứ 4 trong sheet):
                        for (int row = 2; row <= rowCount; row++)
                        {
                            // Đọc dữ liệu từ các ô:
                            var employeeCode = worksheet?.Cells[row, 1]?.Value?.ToString()?.Trim() ?? "";
                            var fullname = worksheet?.Cells[row, 2]?.Value?.ToString()?.Trim() ?? "";
                            var email = worksheet?.Cells[row, 3]?.Value?.ToString()?.Trim() ?? "";
                            var phoneNumber = worksheet?.Cells[row, 5]?.Value?.ToString()?.Trim() ?? "";
                            var identityNumber = worksheet?.Cells[row, 6]?.Value?.ToString()?.Trim() ?? "";
                            var dateOfBirth = worksheet?.Cells[row, 4]?.Value?.ToString()?.Trim() ?? "";
                            var address = worksheet?.Cells[row, 8]?.Value?.ToString()?.Trim() ?? "";
                            var gender = (worksheet?.Cells[row, 7]?.Value?.ToString()?.Trim() ?? "").ToLower() switch
                            {
                                "nam" => 0,
                                "nữ" => 1,
                                "không xác định" => 2,
                                _ => 2,
                            };

                            // Tạo và chèn dữ liệu nhân viên trong excel vào biến lưu trữ:
                            employees.Add(new Employee
                            {
                                EmployeeCode = employeeCode,
                                FullName = fullname,
                                Email = email,
                                PhoneNumber = phoneNumber,
                                IdentityNumber = identityNumber,
                                DateOfBirth = Transfer.ProcessDateTime(dateOfBirth),
                                Gender = gender,
                                Address = address,
                            });
                            count++;
                        }
                    }
                }
            }
            await _repoManager.Employee.InsertMultiAsync(employees);
            return new ResultDetails
            {
                Success = true,
                Data = count,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        #endregion

        #region Method

        public async Task<ResultDetails> SearchEmployeeAsync(string query)
        {
            var res = await _repoManager.Employee.SearchEmployeeAsync(query);
            return new ResultDetails
            {
                Success = true,
                Data = _mapper.Map<List<EmployeeDto>>(res),
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        /// <summary>
        /// Xử lý hợp lệ dữ liệu nhân viên
        /// </summary>
        /// <param name="entity">Nhân viên</param>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        protected override async Task ValidateObject(Employee entity)
        {
            // Kiểm tra mã nhân viên:
            var isDuplicate = await _repoManager.Employee.IsEmployeeCodeExistAsync(entity.EmployeeCode);
            if (isDuplicate)
            {
                throw new ValidateException(Resource.ExceptionsResource.Duplicate_EmployeeCode_Exception);
            }
        }

        #endregion
    }
}
