using Contract.Interfaces;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : RepositoryBase<Employee>, IDepartmentRepository
    {
        public DepartmentRepository(HAUI_2021606204_CaoSyMinhHoangContext context)
            : base(context)
        {

        }
    }
}
