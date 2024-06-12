using BogusDatabaseSeeding.Data;
using BogusDatabaseSeeding.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDb");

// Dependency injections, Add the service for the Database
builder.Services.AddDbContext<EmployeeDbContext>(x => x.UseSqlServer(connectionString));

// Dependency injection for the IDataRepository and DataRepository which contains the definition of the API routes
builder.Services.AddScoped<IDataRepository, DataRepository>();

// Add the DataSeeder class which will seed the Database using Data from Bogus
builder.Services.AddTransient<DataSeeder>();

// Add Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Enable Swagger UI
app.UseSwaggerUI();

// Function that will start database seeding if you run: dotnet run seeddata in the terminal
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    SeedData(app);
}

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}

app.UseSwagger(s => s.SerializeAsV2 = true);

app.MapGet("/", () => "Hello. Please refer to the API documentation on how to use the API");

// GET request to query all the employees in the database
app.MapGet("/api/employees", ([FromServices] IDataRepository db) => db.GetEmployees());

// GET request to query a specific employee using their EmployeeId
app.MapGet("/api/employees/{id}", ([FromServices] IDataRepository db, int id) =>
{
    return db.GetEmployeeById(id);
});

// PUT operation to update employee details
app.MapPut("/api/employees/{id}", ([FromServices] IDataRepository db, EmployeeModel employee) =>
{
    return db.PutEmployee(employee);
});

// POST operation to add a new employee to the datbase
app.MapPost("/api/employees/", ([FromServices] IDataRepository db, EmployeeModel employee) =>
{
    return db.AddEmployee(employee);
});

// DELETE operation to remove an employee from the database
app.MapDelete("/api/employees/{id}", ([FromServices] IDataRepository db, int id) =>
{
    return db.DeleteEmployee(id);
});

app.Run();
