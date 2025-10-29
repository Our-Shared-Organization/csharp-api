using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using whatever_api.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<spaSalonDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("AdminConnection"),
        ServerVersion.Parse("8.0.39-mysql"),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger("/openapi/{documentName}.json");
    app.MapScalarApiReference(options =>
    {
        options.ExpandAllTags()
            .WithTheme(ScalarTheme.BluePlanet);
    });
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();