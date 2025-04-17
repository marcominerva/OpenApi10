using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidation();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", app.Environment.ApplicationName);
    });
}

app.MapPost("/api/todo", (Todo todo) =>
{
    return TypedResults.Ok();
});

app.Run();

[ValidatableType]
public class Todo
{
    [Required]
    [Range(1, 10)]
    public int Id { get; set; }

    [StringLength(10)]
    public required string Title { get; set; }
}