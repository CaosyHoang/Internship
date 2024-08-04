using Contract.Interfaces;

namespace Infrastructure.Repositories
{
    internal sealed class PositionRepository : RepositoryBase<Employee>, IPositionRepository
    {
        public PositionRepository(RepositoryContext context)
            : base(context)
        {

        }
    }
}
