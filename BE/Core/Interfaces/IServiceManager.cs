namespace Core.Interfaces
{
    public interface IServiceManager
    {
        IDepartmentService DepartmentService { get; }
        IEmployeeService EmployeeService { get; }
        IPositionService PositionService { get; }
    }
}
