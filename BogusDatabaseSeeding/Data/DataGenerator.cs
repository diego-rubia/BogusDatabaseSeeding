using Bogus;
using BogusDatabaseSeeding.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BogusDatabaseSeeding.Data;

public class DataGenerator
{
    Faker<EmployeeModel> employeeModelFake;

    public DataGenerator()
    {
        // Provide the seed for the Randomizer
        const int seed = 123;
        Randomizer.Seed = new Random(seed);

        // Specify the rules for generating the Employee data using Bogus,,,, .RuleFor(e => e.EmployeeID, f => f.Random.Int(1, 10000))
        employeeModelFake = new Faker<EmployeeModel>()
            .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            .RuleFor(e => e.LastName, f => f.Name.LastName())
            .RuleFor(e => e.Address, f => f.Address.StreetAddress())
            .RuleFor(e => e.Remarks, f => f.Lorem.Word())
            .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
            .RuleFor(e => e.EmployeeDepartment, f => f.PickRandom<Department>())
            .RuleFor(e => e.EmployeePerformanceRating, f => f.PickRandom<PerformanceRating>());
    }

    // Function that generates the data
    public List<EmployeeModel> GenerateEmployees(int noOfEmployees)
    {
        return employeeModelFake.Generate(noOfEmployees);
    }
}
