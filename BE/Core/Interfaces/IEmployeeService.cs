using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Tìm kiếm nhân viên
        /// </summary>
        /// <param name="query">Key tìm kiếm</param>
        /// <returns>Chi tiết kết quả</returns>
        Task<ResultDetails> SearchEmployeeAsync(string query);
    }
}
