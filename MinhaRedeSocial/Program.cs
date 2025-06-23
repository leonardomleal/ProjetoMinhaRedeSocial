using Microsoft.EntityFrameworkCore;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Services.Usuario;
using MinhaRedeSocial.Domain.Contratos.Repositorios;
using MinhaRedeSocial.Infra.Dados;
using MinhaRedeSocial.Infra.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DadosContext>(options => options.UseSqlite("Data Source=MinhaRedeSocial.db"));
//Services
builder.Services.AddScoped<IBuscarUsuarioService, BuscarUsuarioService>();
builder.Services.AddScoped<ICadastrarUsuarioService, CadastrarUsuarioService>();
//Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

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
