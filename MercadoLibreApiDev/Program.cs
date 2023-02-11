using DataEF.Context;
using Microsoft.EntityFrameworkCore;
using MLApiServices;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("MLApiDb"); ;

// Add services to the container.
builder.Services.AddDbContext<MLApiDbContext>(
    options=> options.UseSqlServer(connectionstring, sqlServerOptions => sqlServerOptions.MigrationsAssembly("DataEF"))
    );

//DI for Services and mappers
DIBuilderService.ConfigureDIServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
