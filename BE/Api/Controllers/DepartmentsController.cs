﻿using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Declaration

        private readonly IServiceManager _serviceManager;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public DepartmentsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy toàn bộ phòng ban
        /// </summary>
        /// <returns>Chi tiết request</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _serviceManager.DepartmentService.GetAllAsync<DepartmentDto>();
            return Ok(res);
        }

        #endregion
    }
}
