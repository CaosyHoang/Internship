using Contracts;
using Core.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Controllers
{
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDapperContext _dapper;
        private readonly IServiceManager _service;
        private readonly ILogger _logger;

        public EmployeesController(IDapperContext dapper, IServiceManager service, ILogger logger)
        {
            _dapper = dapper;
            _service = service;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Custommers()
        {
            //string sql = $"SELECT * FROM Employee";
            //var data = _dapper.Connection.Query(sql);

            //return Ok(data);
            throw new Exception("Exception");
        }
    }
}
