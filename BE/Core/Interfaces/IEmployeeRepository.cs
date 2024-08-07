using Core.Entities;

namespace Core.Interfaces
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        /// <summary>
        /// Kiểm tra tồn tại mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>Nếu đã tồn tại trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<bool> IsEmployeeCodeExistAsync(string employeeCode);
        /// <summary>
        /// Tìm kiếm nhân viên
        /// </summary>
        /// <param name="query">Key tìm kiếm</param>
        /// <returns>Danh sách nhân viên tìm được</returns>
        Task<IEnumerable<Employee>> SearchEmployeeAsync(string query);
    }
}
