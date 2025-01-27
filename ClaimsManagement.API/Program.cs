﻿using ClaimsManagement.Application.UseCases;
using ClaimsManagement.Application.UsesCases;
using ClaimsManagement.Domain.Interfaces;
using ClaimsManagement.Infrastructure.Data;
using ClaimsManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajouter les cas d'utilisation (UseCases)
builder.Services.AddScoped<CreateInsuredUseCase>();
builder.Services.AddScoped<GetClaimByIdUseCase>();
builder.Services.AddScoped<GetClaimsByInsuredIdUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<SubmitClaimUseCase>();
builder.Services.AddScoped<UpdateClaimUseCase>();
builder.Services.AddScoped<UpdateClaimStatusUseCase>();
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IInsuredRepository, InsuredRepository>();

// Définir la chaîne de connexion directement dans le code
var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ClaimsDB;Trusted_Connection=True;";

// Ajouter le contexte de base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
           .LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors(); // Active CORS ici
app.UseRouting();

// Configurer l'application
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
