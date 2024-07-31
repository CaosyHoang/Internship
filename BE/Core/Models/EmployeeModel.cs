namespace Core.Models
{
    public class EmployeeModel
    {
        public Guid EmployeeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public string EmployeeCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string IdentityNumber { get; set; } = null!;
        public DateOnly? IdentityDate { get; set; }
        public string? IndentityPlace { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? LandlineNumber { get; set; }
        public string Email { get; set; } = null!;
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public decimal? Salary { get; set; }
    }
}
