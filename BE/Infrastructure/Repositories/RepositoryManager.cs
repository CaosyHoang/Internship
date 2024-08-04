using Contract.Interfaces;
using MySqlConnector;
using System.Data;

namespace Infrastructure.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IPositionRepository> _PositionRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(context));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
            _PositionRepository = new Lazy<IPositionRepository>(() => new PositionRepository(context));
        }
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IPositionRepository Position => _PositionRepository.Value;
        public void Save() => _context.SaveChanges();
    }
}
