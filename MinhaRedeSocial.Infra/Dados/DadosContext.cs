using Microsoft.EntityFrameworkCore;
using MinhaRedeSocial.Domain.Models.Amigos;
using MinhaRedeSocial.Domain.Models.Usuarios;

namespace MinhaRedeSocial.Infra.Dados;

public class DadosContext(DbContextOptions<DadosContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Amigo> Amigos => Set<Amigo>();
    //public DbSet<Solicitante> Solicitantes => Set<Solicitante>();
    //public DbSet<Solicitacao> Solicitacoes => Set<Solicitacao>();
    //public DbSet<Comentario> Comentarios => Set<Comentario>();
    //public DbSet<Postagem> Postagens => Set<Postagem>();

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
        modelBuilder.Entity<Usuario>().Ignore(x => x.Solicitacoes);
        modelBuilder.Entity<Usuario>().Ignore(x => x.Amigos);

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

        #region Amigos
        //Configurações de propriedades.
        modelBuilder.Entity<Amigo>()
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Amigos)
            .HasForeignKey(p => p.UsuarioId);

        modelBuilder.Entity<Amigo>()
            .HasOne(x => x.UsuarioAmigo)
            .WithMany(x => x.Amigos)
            .HasForeignKey(p => p.UsuarioAmigoId);

        //Ignore propriedades.
        modelBuilder.Entity<Amigo>().Ignore(x => x.Usuario);
        modelBuilder.Entity<Amigo>().Ignore(x => x.UsuarioAmigo);

        //Definindo chave primária.
        modelBuilder.Entity<Amigo>().HasKey(x => x.Id);
        #endregion
    }
}