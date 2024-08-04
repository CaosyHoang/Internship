using Contract.Interfaces;

namespace Infrastructure.Repositories
{
    internal sealed class DepartmentRepository : RepositoryBase<Employee>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext context)
            : base(context)
        {

        }
    }
}
