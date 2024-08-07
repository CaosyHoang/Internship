using Core.Interfaces;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        #region Declaration

        private readonly IDapperContext _context;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IPositionRepository> _positionRepository;

        #endregion

        #region Constructor

        public RepositoryManager(IDapperContext context)
        {
            _context = context;
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(context));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
            _positionRepository = new Lazy<IPositionRepository>(() => new PositionRepository(context));
        }

        #endregion

        #region Method
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IPositionRepository Position => _positionRepository.Value;

        #endregion
    }
}
