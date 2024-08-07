using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/positions")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        #region Declaration

        private readonly IServiceManager _serviceManager;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public PositionsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy toàn bộ vị trí/ chức vụ
        /// </summary>
        /// <returns>Chi tiết kết quả</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _serviceManager.DepartmentService.GetAllAsync();
            return Ok(res);
        }

        #endregion
    }
}
