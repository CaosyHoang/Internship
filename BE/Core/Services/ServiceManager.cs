

using AutoMapper;
using Contract.Interfaces;
using Core.Interfaces;

namespace Core.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IPositionService> _positionService;

        public ServiceManager(IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repo, logger, mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repo, logger, mapper));
            _positionService = new Lazy<IPositionService>(() => new PositionService(repo, logger, mapper));
        }

        public IDepartmentService DepartmentService => _departmentService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IPositionService PositionService => _positionService.Value;
    }
}
