using BankAppBackend.Models;
using BankAppBackend.Repositories;
using BankAppBackend.Repositories.Interfaces;
using BankAppBackend.Service;
using BankAppBackend.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
builder.Services.AddScoped<ITellerRepository, TellerRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TranscationRepository>();
builder.Services.AddScoped<IApplicantService, ApplicantService>();
builder.Services.AddScoped<ITellerService, TellerService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IRedisMessagePublisherService, RedisMessagePublisherService>();

string connectionString = builder.Configuration.GetConnectionString("SQLConnectionString") ?? throw new InvalidOperationException("Connection string of name SQLConnectionString not found");
builder.Services.AddDbContext<DatabaseContext>(conn => conn.UseSqlServer(connectionString));
// Apply migrations
var app = builder.Build();
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
if (context.Database.GetPendingMigrations().Any())
{
    await context.Database.MigrateAsync();
}

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
