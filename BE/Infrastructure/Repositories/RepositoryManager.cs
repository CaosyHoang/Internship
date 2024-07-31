using Contract.Interfaces;

namespace Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly HAUI_2021606204_CaoSyMinhHoangContext _context;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IPositionRepository> _PositionRepository;

        public RepositoryManager(HAUI_2021606204_CaoSyMinhHoangContext context)
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
