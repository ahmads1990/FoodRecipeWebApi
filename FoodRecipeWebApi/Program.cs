using Autofac.Extensions.DependencyInjection;
using Autofac;
using FoodRecipeWebApi.Config;
using FoodRecipeWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using FoodRecipeWebApi.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new AutofacModule());
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
        .UseSqlServer(connectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
        .EnableSensitiveDataLogging();
});

var app = builder.Build();
AutoMapperServices.Mapper = app.Services.GetRequiredService<IMapper>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
