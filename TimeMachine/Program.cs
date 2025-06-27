using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TimeMachine.DataAccessLayer;
using TimeMachine.Models;
using TinyHelpers.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("SqlConnection"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultProblemDetails();
builder.Services.AddDefaultExceptionHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/people/{id:guid}", async (Guid id, DateTime? dateTime, ApplicationDbContext dbContext) =>
{
    var people = dateTime.HasValue
        ? dbContext.People.TemporalAsOf(dateTime.Value)
        : dbContext.People;

    var person = await people
        .Include(p => p.PhoneNumbers)
        .Select(p => new Person
        {
            Id = p.Id,
            Name = p.Name,
            City = new()
            {
                Id = p.City.Id,
                Name = p.City.Name
            },
            PhoneNumbers = p.PhoneNumbers.Select(pn => new PhoneNumber
            {
                Id = pn.Id,
                Type = pn.Type,
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
.Produces(StatusCodes.Status404NotFound)
.WithOpenApi();

app.Run();
