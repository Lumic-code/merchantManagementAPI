using FluentValidation;
using MerchantManagement.API.Endpoints;
using MerchantManagement.App.Merchants.Commands.Create;
using MerchantManagement.App.Merchants.Validators;
using MerchantManagement.Infra;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("MerchantDb"));
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<CreateMerchantCommandHandler>();
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateMerchantCommandValidator>();
builder.Services.AddHttpClient();
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

app.MapMerchantEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
