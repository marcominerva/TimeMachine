using Microsoft.EntityFrameworkCore;
using MinimalHelpers.OpenApi;
using TimeMachine.DataAccessLayer;
using TimeMachine.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("SqlConnection"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddMissingSchemas();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/people/{id:guid}", async (Guid id, ApplicationDbContext dbContext) =>
{
    var person = await dbContext.People.Include(p => p.PhoneNumbers)
        .Select(p => new Person
        {
            Id = p.Id,
            Name = p.Name,
            City = p.City,
            PhoneNumbers = p.PhoneNumbers.Select(pn => new PhoneNumber
            {
                Id = pn.Id,
                Number = pn.Number
            })
        })
        .FirstOrDefaultAsync(p => p.Id == id);

    if (person is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(person);
})
.Produces<Person>()
.WithOpenApi();

app.Run();
