using BogusDatabaseSeeding.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BogusDatabaseSeeding.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly EmployeeDbContext db;

        public DataRepository(EmployeeDbContext db) 
        {
            this.db = db;
        }
    
        public List<EmployeeModel> GetEmployees()
        {
            //// Pagination parameters, hard-coded for now
            //// Uncomment to return the employees in paginated form
            //int pageNumber = 1;
            //int pageSize = 20;

            //var skipNumber = (pageNumber - 1) * pageSize;
            //return db.Employees
            //    .Skip(skipNumber)
            //    .Take(pageSize)
            //    .ToList();
            return db.Employees.ToList();
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            // Use Where instead of Select
            return db.Employees.Where(e => e.EmployeeID == id).FirstOrDefault();
        }
        
        public EmployeeModel PutEmployee(EmployeeModel employee)
        {
            db.Employees.Update(employee);
            db.SaveChanges();
            // Show the modified Employee data from the database
            return db.Employees.Where(e => e.EmployeeID == employee.EmployeeID).FirstOrDefault();
        }

        public IQueryable<EmployeeModel> AddEmployee(EmployeeModel employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            // Show the added Employee data from the database. Note that when Posting data you don't need to specify the EmployeeId
            // Note that only the last 5 database entries will be shown in response
            return db.Employees.OrderByDescending(e => e.EmployeeID).Take(5);
        }

        public int DeleteEmployee(int id)
        {
            // Note that ExecuteDelete() returns an int
            var deleteEmployee = db.Employees.Where(e => e.EmployeeID == id).ExecuteDelete();
            db.SaveChanges();
            return deleteEmployee;
        }
    }
}
