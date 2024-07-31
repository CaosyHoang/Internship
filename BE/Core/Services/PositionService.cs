using AutoMapper;
using Contract.Interfaces;
using Core.Interfaces;

namespace Core.Services
{
    public sealed class PositionService : IPositionService
    {
        private readonly IRepositoryManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PositionService(IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
