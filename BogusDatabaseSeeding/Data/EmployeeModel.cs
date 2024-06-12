using BogusDatabaseSeeding.Data;

namespace BogusDatabaseSeeding.Model
{
    public record EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public string Email { get; set; }
        public Department EmployeeDepartment { get; set; }
        public PerformanceRating EmployeePerformanceRating { get; set; }
    }
}
