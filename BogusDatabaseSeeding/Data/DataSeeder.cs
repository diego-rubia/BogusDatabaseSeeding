﻿using BogusDatabaseSeeding.Model;

namespace BogusDatabaseSeeding.Data
{
    public class DataSeeder
    {
        private readonly EmployeeDbContext employeeDbContext;

        public DataSeeder(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        public void Seed()
        {
            // Check if the database is empty, note that the table name can be found in migrationBuilder
            if (!employeeDbContext.Employees.Any())
            {
                // Generate the data using Bogus, for now generate 100 employees
                DataGenerator employeeGenerator = new DataGenerator();
                var employees = employeeGenerator.GenerateEmployees(100);
                // Add the employees generated by Bogus to the database
                employeeDbContext.Employees.AddRange(employees);
                employeeDbContext.SaveChanges();
            }
        }
    }
}