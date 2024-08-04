using AutoMapper;
using Contract.Interfaces;
using Core.Interfaces;

namespace Core.Services
{
    internal sealed class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager _repo;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DepartmentService(IRepositoryManager repo, ILoggerManager logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
