using Core.Entities;
using Core.Interfaces;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    internal sealed class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor
        public DepartmentRepository(IDapperContext context)
            : base(context)
        {

        }

        #endregion

        #region Method
        #endregion
    }
}
