using Contract.Interfaces;

namespace Infrastructure.Repositories
{
    internal sealed class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context)
            : base(context)
        {

        }
    }
}
