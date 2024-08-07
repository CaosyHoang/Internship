using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    internal sealed class DepartmentService : BaseService<Department>, IDepartmentService
    {
        #region Declaration

        private readonly IRepositoryManager _repo;
        private readonly ILoggerManager _logger;

        #endregion

        #region Property
        #endregion

        #region Constructor
        public DepartmentService(IRepositoryManager repo, ILoggerManager logger, IMapper mapper) : base(repo.Department, mapper)
        {
            _repo = repo;
            _logger = logger;
        }

        #endregion

        #region Method
        #endregion


    }
}
