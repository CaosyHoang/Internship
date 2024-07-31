using Contract.Interfaces;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HAUI_2021606204_CaoSyMinhHoangContext context)
            : base(context)
        {

        }
    }
}
