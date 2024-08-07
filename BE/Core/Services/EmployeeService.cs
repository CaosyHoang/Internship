using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
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

        #endregion

        #region Method

        public async Task<ResultDetails> SearchEmployeeAsync(string query)
        {
            var res = await _repoManager.Employee.SearchEmployeeAsync(query);
            return new ResultDetails
            {
                Success = true,
                Data = res,
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
