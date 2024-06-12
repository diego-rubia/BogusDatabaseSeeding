using BogusDatabaseSeeding.Model;

namespace BogusDatabaseSeeding.Data
{
    public interface IDataRepository
    {
        public List<EmployeeModel> GetEmployees();

        public EmployeeModel GetEmployeeById(int id);

        public EmployeeModel PutEmployee(EmployeeModel employee);

        public IQueryable<EmployeeModel> AddEmployee(EmployeeModel employee);

        public int DeleteEmployee(int id);
    }
}