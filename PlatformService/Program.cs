using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// SQL Server DB Connection
//builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));

// InMemory Server DB Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Inmemory"));

// Add services to the container.
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

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

// PrepPopulation(app);
PrepDb.PrepPopulation(app);

app.Run();
