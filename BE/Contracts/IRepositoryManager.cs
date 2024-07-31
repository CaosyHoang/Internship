namespace Contract.Interfaces
{
    public interface IRepositoryManager
    {
        IDepartmentRepository Department { get; }
        IPositionRepository Position { get; }
        IEmployeeRepository Employee { get; }
        void Save();
    }
}
