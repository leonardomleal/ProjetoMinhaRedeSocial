using Microsoft.EntityFrameworkCore;
using MinhaRedeSocial.Aplicacao.Contratos.Services;
using MinhaRedeSocial.Aplicacao.Services.Postagem;
using MinhaRedeSocial.Aplicacao.Services.Solicitacao;
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
builder.Services.AddScoped<IPesquisarUsuariosService, PesquisarUsuariosService>();
builder.Services.AddScoped<IPesquisarUsuariosPaginadoService, PesquisarUsuariosPaginadoService>();
builder.Services.AddScoped<ICadastrarUsuarioService, CadastrarUsuarioService>();
builder.Services.AddScoped<IBuscarSolicitacoesPorUsuarioService, BuscarSolicitacoesPorUsuarioService>();
builder.Services.AddScoped<ICadastrarPostagemService, CadastrarPostagemService>();
//Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();
builder.Services.AddScoped<IPostagemRepository, PostagemRepository>();

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
