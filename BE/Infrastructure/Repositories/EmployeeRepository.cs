using Infrastructure.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.VisualBasic.FileIO;
using Core.DTOs;

namespace Infrastructure.Repositories
{
    internal sealed class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor

        public EmployeeRepository(IDapperContext context)
            : base(context)
        {

        }

        #endregion

        #region Method

        public async Task<bool> IsEmployeeCodeExistAsync(string employeeCode)
        {
            var res = await CheckExistAsync(employeeCode);
            return res;
        }

        public async Task<IEnumerable<Employee>> SearchEmployeeAsync(string query)
        {
            var res = new List<Employee>();
            // Lấy ra tất cả nhân viên:
            var employees = await GetAsync();
            // Lấy ra các props tìm kiếm:
            var props = typeof(SearchEmployee).GetProperties();
            // Duyệt từng prop:
            foreach (var prop in props)
            {
                // Lấy ra prop của Employee theo tên prop của searchEmployee:
                var employeeProp = typeof(Employee).GetProperty(prop.Name);
                if (employeeProp != null)
                {
                    foreach (var employee in employees)
                    {
                        // Lấy giá trị của prop:
                        var value = employeeProp.GetValue(employee)?.ToString()?.ToLower();
                        if (value != null && value.Contains(query.ToLower()))
                        {
                            res.Add(employee);
                        }
                    }
                }
            }
            return res;
        }

        #endregion
    }
}
