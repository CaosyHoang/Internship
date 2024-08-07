using Infrastructure.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    internal sealed class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        #region Declaration
        #endregion

        #region Property
        #endregion

        #region Constructor

        public PositionRepository(IDapperContext context)
                : base(context)
        {

        }

        #endregion

        #region Method
        #endregion
    }
}



