using cadastro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using cadastro.Application.Interfaces;
using cadastro.Application.Services;
using cadastro.Infrastructure.Repositories;
using cadastro.Application.Validators;
using FluentValidation;
using cadastro.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>
(options =>
options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddTransient<IValidator<UsuarioCreateDto>, UsuarioCreateValidator>();
builder.Services.AddTransient<IValidator<UsuarioUpdateDto>, UsuarioUpdateValidator>();
builder.Services.AddTransient<IValidator<UsuarioUpdateDto>, UsuarioPatchValidator>();

var app = builder.Build();













if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

