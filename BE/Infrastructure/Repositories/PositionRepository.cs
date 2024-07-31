using Contract.Interfaces;

namespace Infrastructure.Repositories
{
    public class PositionRepository : RepositoryBase<Employee>, IPositionRepository
    {
        public PositionRepository(HAUI_2021606204_CaoSyMinhHoangContext context)
            : base(context)
        {

        }
    }
}
