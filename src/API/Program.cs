using Application;
using Application.Interfaces;
using Models;
using Repositories;
using Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connStr = builder.Configuration.GetConnectionString("DefaultConnection") 
              ?? throw new InvalidOperationException("DefaultConnection connection string configuration is missing.");


builder.Services.AddTransient<IPhoneNumberRepository, PhoneNumberRepository>(_ => new PhoneNumberRepository(connStr!));
builder.Services.AddTransient<IClientRepository, ClientRepository>(_ => new ClientRepository(connStr!));
builder.Services.AddTransient<IOperatorsService, OperatorsesService>();

builder.Services.AddControllers();
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