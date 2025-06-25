using Microsoft.EntityFrameworkCore;
using MinhaRedeSocial.Domain.Models.Amigos;
using MinhaRedeSocial.Domain.Models.Postagens;
using MinhaRedeSocial.Domain.Models.Solicitacoes;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Infra.Dados;

public class DadosContext(DbContextOptions<DadosContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Amigo> Amigos => Set<Amigo>();
    public DbSet<Solicitante> Solicitantes => Set<Solicitante>();
    public DbSet<Solicitacao> Solicitacoes => Set<Solicitacao>();
    public DbSet<Comentario> Comentarios => Set<Comentario>();
    public DbSet<Postagem> Postagens => Set<Postagem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Usuario
        //Definindo chave primária.
        modelBuilder.Entity<Usuario>().HasKey(x => x.Id);
        #endregion

        #region Amigo
        //Definindo chave primária.
        modelBuilder.Entity<Amigo>().HasKey(x => x.Id);

        //Configurações chaves estrangeiras.
        modelBuilder.Entity<Amigo>()
            .HasOne(x => x.Usuario)
            .WithOne(x => x.Amigo)
            .HasForeignKey<Amigo>(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region Solicitacao
        //Definindo chave primária.
        modelBuilder.Entity<Solicitacao>().HasKey(x => x.Id);
        #endregion

        #region Solicitante
        //Definindo chave primária.
        modelBuilder.Entity<Solicitante>().HasKey(x => x.Id);
        #endregion

        #region Comentario
        //Definindo chave primária.
        modelBuilder.Entity<Comentario>().HasKey(x => x.Id);
        #endregion

        #region Postagem
        //Definindo chave primária.
        modelBuilder.Entity<Postagem>().HasKey(x => x.Id);
        #endregion
    }
}