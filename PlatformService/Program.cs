using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.IsProduction())
{
    // Add services to the container.
    // SQL Server DB Connection
    Console.WriteLine("--> Usando SQL Server DB");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
} 
else 
{
    // InMemory Server DB Connection
    Console.WriteLine("--> Usando Inmemory DB");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("Inmemory"));
}

// Add services to the container.

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// PrepPopulation(app);
PrepDb.PrepPopulation(app);

app.Run();