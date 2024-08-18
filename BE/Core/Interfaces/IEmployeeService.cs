using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Tìm kiếm nhân viên
        /// </summary>
        /// <param name="query">Key tìm kiếm</param>
        /// <returns>Trả về các bản ghi tìm kiếm được</returns>
        Task<ResultDetails> SearchEmployeeAsync(string query);
        /// <summary>
        /// Thực hiện nhập dữ liệu nhân viên
        /// </summary>
        /// <param name="file">File excel dữ liệu nhập khẩu</param>
        /// <returns>Số ban ghi thêm thành công</returns>
        Task<ResultDetails> ImportEmployeeAsync(IFormFile file);
        /// <summary>
        /// Thực hiện xuất dữ liệu nhân viên viên ra excel
        /// </summary>
        /// <returns>Tệp Excel chứa dữ liệu nhân viên</returns>
        Task<ResultDetails> ExportEmployeeAsync();
    }
}
