using Microsoft.EntityFrameworkCore;
using whatever_api.Model;
using System.Text.Json.Serialization;

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run("http://localhost:5115");