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
        #region Amigo
        modelBuilder.Entity<Amigo>()
            .HasOne(x => x.Usuario)
            .WithOne(x => x.Amigo)
            .HasForeignKey<Amigo>(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region Solicitante
        modelBuilder.Entity<Solicitante>()
            .HasOne(x => x.Usuario)
            .WithOne(x => x.Solicitante)
            .HasForeignKey<Solicitante>(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion
    }
}