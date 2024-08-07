using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace Core.Services
{
    internal sealed class PositionService : BaseService<Position>, IPositionService
    {
        #region Declaration

        private readonly IRepositoryManager _repo;
        private readonly ILoggerManager _logger;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public PositionService(IRepositoryManager repo, ILoggerManager logger, IMapper mapper) : base(repo.Position, mapper)
        {
            _repo = repo;
            _logger = logger;
        }

        #endregion

        #region Method
        #endregion


    }
}
