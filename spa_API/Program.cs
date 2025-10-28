using Microsoft.EntityFrameworkCore;
using whatever_api.Model;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<SpasalonContext>(options =>
    options.UseLazyLoadingProxies()
           .UseMySql(builder.Configuration.GetConnectionString("AdminConnection"),
                    ServerVersion.Parse("8.0.40-mysql")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();
app.MapControllers();
app.Run();