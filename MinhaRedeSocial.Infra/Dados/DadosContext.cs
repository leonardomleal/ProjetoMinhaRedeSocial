using Microsoft.EntityFrameworkCore;
using MinhaRedeSocial.Domain.Models.Amigos;
using MinhaRedeSocial.Domain.Models.Postagens;
using MinhaRedeSocial.Domain.Models.Solicitacoes;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Infra.Dados;

public class DadosContext(DbContextOptions<DadosContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Amizade> Amizades => Set<Amizade>();
    public DbSet<Amigo> Amigos => Set<Amigo>();
    public DbSet<Solicitacao> Solicitacoes => Set<Solicitacao>();
    public DbSet<Solicitante> Solicitantes => Set<Solicitante>();
    public DbSet<Comentario> Comentarios => Set<Comentario>();
    public DbSet<Postagem> Postagens => Set<Postagem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Usuario
        //Configurações de propriedades.
        //modelBuilder.Entity<Usuario>(entity =>
        //{
        //    entity.Property(x => x.Id).IsRequired();
        //    entity.Property(x => x.Nome).IsRequired();
        //    entity.Property(x => x.Email).IsRequired();
        //    entity.Property(x => x.Apelido).IsRequired(false);
        //    entity.Property(x => x.DataNascimento).IsRequired();
        //    entity.Property(x => x.Cep).IsRequired();
        //    entity.Property(x => x.Senha).IsRequired();
        //    entity.Property(x => x.Foto).IsRequired(false);
        //});

        //Ignore propriedades.
        modelBuilder.Entity<Usuario>().Ignore(x => x.Amizades);
        modelBuilder.Entity<Usuario>().Ignore(x => x.Amigo);
        modelBuilder.Entity<Usuario>().Ignore(x => x.Solicitacaos);
        modelBuilder.Entity<Usuario>().Ignore(x => x.Solicitante);

        //Definindo chave primária.
        modelBuilder.Entity<Usuario>().HasKey(x => x.Id);

        //Inserindo dados iniciais.
        //modelBuilder.Entity<Usuario>().HasData(
        //    new Usuario("Leonardo", "leonardo.moises@cwi.com.br", "Léo", DateTime.Parse("1992-04-15T00:00:00"), string.Empty, string.Empty, string.Empty),
        //    new Usuario("Vinicius", "vinicius@cwi.com.br", "Vini", DateTime.Parse("2000-01-01T00:00:00"), string.Empty, string.Empty, string.Empty),
        //    new Usuario("João", "joao@cwi.com.br", string.Empty, DateTime.Parse("2000-01-01T00:00:00"), string.Empty, string.Empty, string.Empty),
        //    new Usuario("Renan", "renan@cwi.com.br", string.Empty, DateTime.Parse("2000-01-01T00:00:00"), string.Empty, string.Empty, string.Empty),
        //    new Usuario("Rômulo", "romulo@cwi.com.br", string.Empty, DateTime.Parse("2000-01-01T00:00:00"), string.Empty, string.Empty, string.Empty)
        //);
        #endregion

        #region Amizade
        //Ignore propriedades.
        modelBuilder.Entity<Amizade>().Ignore(x => x.Usuario);
        modelBuilder.Entity<Amizade>().Ignore(x => x.Amigo);

        //Definindo chave primária.
        modelBuilder.Entity<Amizade>().HasKey(x => x.Id);

        //Configurações chaves estrangeiras.
        modelBuilder.Entity<Amizade>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Amizades)
            .HasForeignKey(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region Amigo
        //Ignore propriedades.
        modelBuilder.Entity<Amigo>().Ignore(x => x.Usuario);
        modelBuilder.Entity<Amigo>().Ignore(x => x.Amizade);

        //Definindo chave primária.
        modelBuilder.Entity<Amigo>().HasKey(x => x.Id);

        //Configurações chaves estrangeiras.
        modelBuilder.Entity<Amigo>()
            .HasOne(x => x.Amizade)
            .WithOne(x => x.Amigo)
            .HasForeignKey<Amigo>(p => p.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Amigo>()
            .HasOne(x => x.Usuario)
            .WithOne(x => x.Amigo)
            .HasForeignKey<Amigo>(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region Solicitacao
        //Ignore propriedades.
        modelBuilder.Entity<Solicitacao>().Ignore(x => x.Usuario);
        modelBuilder.Entity<Solicitacao>().Ignore(x => x.Solicitante);

        //Definindo chave primária.
        modelBuilder.Entity<Solicitacao>().HasKey(x => x.Id);

        //Configurações chaves estrangeiras.
        modelBuilder.Entity<Solicitacao>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Solicitacaos)
            .HasForeignKey(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region Solicitante
        //Ignore propriedades.
        modelBuilder.Entity<Solicitante>().Ignore(x => x.Usuario);
        modelBuilder.Entity<Solicitante>().Ignore(x => x.Solicitacao);

        //Definindo chave primária.
        modelBuilder.Entity<Solicitante>().HasKey(x => x.Id);

        //Configurações chaves estrangeiras.
        modelBuilder.Entity<Solicitante>()
            .HasOne(x => x.Solicitacao)
            .WithOne(x => x.Solicitante)
            .HasForeignKey<Solicitante>(p => p.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Solicitante>()
            .HasOne(x => x.Usuario)
            .WithOne(x => x.Solicitante)
            .HasForeignKey<Solicitante>(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region Comentario
        //Ignore propriedades.
        modelBuilder.Entity<Comentario>().Ignore(x => x.Usuario);
        modelBuilder.Entity<Comentario>().Ignore(x => x.Postagem);

        //Definindo chave primária.
        modelBuilder.Entity<Comentario>().HasKey(x => x.Id);

        //Configurações chaves estrangeiras.
        modelBuilder.Entity<Comentario>()
            .HasOne(x => x.Usuario)
            .WithOne(x => x.Comentario)
            .HasForeignKey<Comentario>(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Comentario>()
            .HasOne(x => x.Postagem)
            .WithMany(x => x.Comentarios)
            .HasForeignKey(p => p.PostagemId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion

        #region Postagem
        //Ignore propriedades.
        modelBuilder.Entity<Postagem>().Ignore(x => x.Usuario);
        modelBuilder.Entity<Postagem>().Ignore(x => x.Comentarios);

        //Definindo chave primária.
        modelBuilder.Entity<Postagem>().HasKey(x => x.Id);

        //Configurações chaves estrangeiras.
        modelBuilder.Entity<Postagem>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Postagens)
            .HasForeignKey(p => p.UsuarioId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);
        #endregion
    }
}