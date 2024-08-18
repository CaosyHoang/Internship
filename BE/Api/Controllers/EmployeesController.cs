using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Declaration

        private readonly IServiceManager _serviceManager;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public EmployeesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #endregion

        #region Method

        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        /// <param name="model">Nhân viên (EmployeeDto)</param>
        /// <returns>Chi tiết request</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto model)
        {
            // Ánh xạ về kiểu Employee:
            var data = _serviceManager.EmployeeService.MappingObject<Employee>(model);
            var res = await _serviceManager.EmployeeService.InsertAsync(data);
            return Ok(res);
        }
        /// <summary>
        /// Lấy thông tin nhân viên theo Id
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>Chi tiết request</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await _serviceManager.EmployeeService.GetAsync<EmployeeDto>(id);
            return Ok(res);
        }
        /// <summary>
        /// Lấy ra số lượng nhân viên
        /// </summary>
        /// <returns>Chi tiết request</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var res = await _serviceManager.EmployeeService.CountData();
            return Ok(res);
        }
        /// <summary>
        /// Lấy ra danh sách nhân viên
        /// </summary>
        /// <param name="limit">Số nhân viên tối đa</param>
        /// <param name="number">Trang số</param>
        /// <returns>Chi tiết request</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        [HttpGet("page")]
        public async Task<IActionResult> GetPage(int limit, int number = 1)
        {
            var res = await _serviceManager.EmployeeService.LoadDataAsync<EmployeeDto>(limit, number);
            return Ok(res);
        }
        /// <summary>
        /// Tìm kiếm nhân viên
        /// </summary>
        /// <param name="queryString">Chuỗi được so sánh với các field search đã thiết lập</param>
        /// <returns>Chi tiết request</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        [HttpGet("search")]
        public async Task<IActionResult> Search(string queryString)
        {
            var res = await _serviceManager.EmployeeService.SearchEmployeeAsync(queryString);
            return Ok(res);
        }
        /// <summary>
        /// Xóa một nhân viên
        /// </summary>
        /// <param name="id">Id nhân viên</param>
        /// <returns>Chi tiết request</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _serviceManager.EmployeeService.DeleteAsync(id);
            return Ok(res);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] EmployeeDto model)
        {
            // Ánh xạ về kiểu Employee:
            var data = _serviceManager.EmployeeService.MappingObject<Employee>(model);
            var res = await _serviceManager.EmployeeService.UpdateAsync(data);
            return Ok(res);
        }
        #endregion
    }
}
