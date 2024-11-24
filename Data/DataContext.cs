using Loja_Projeto.Model;
using Microsoft.EntityFrameworkCore;

namespace Loja_Projeto.Data
{
    public class DataContext : DbContext
    {
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<TrocaModel> Trocas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Categoria
            modelBuilder.Entity<CategoriaModel>(entity =>
            {
                entity.ToTable("Categoria");

                entity.HasKey(e => e.CategoriaId);
                entity.Property(e => e.CategoriaId).ValueGeneratedOnAdd();

                entity.Property(e => e.NomeCategoria)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(e => e.NomeCategoria)
                      .IsUnique();
            });

            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel() { CategoriaId = 1, NomeCategoria = "Celular" },
                new CategoriaModel() { CategoriaId = 2, NomeCategoria = "Gadgets" }
            );
            #endregion

            #region Usuario
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.UsuarioId);

                entity.Property(e => e.UsuarioId).ValueGeneratedOnAdd();

                entity.Property(e => e.NomeUsuario)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.EmailUsuario)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Senha)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Regra)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(e => e.NomeUsuario)
                      .IsUnique();
            });

            modelBuilder.Entity<UsuarioModel>().HasData(
                new UsuarioModel(1, "admin@admin", "Admin", "admin123", "admin"),
                new UsuarioModel(2, "fabriciorosanet@gmail.com", "Fabricio Rosa", "123456", "admin")
            );
            #endregion

            #region Produto
            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("Produto");

                entity.HasKey(e => e.ProdutoId);
                entity.Property(e => e.ProdutoId).ValueGeneratedOnAdd();

                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.SugestaoTroca)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Valor)
                      .IsRequired()
                      .HasPrecision(18, 2);

                entity.Property(e => e.DataCadastro)
                      .IsRequired();

                entity.Property(e => e.DataExpiracao)
                      .IsRequired();

                // Relacionamento Categoria
                entity.HasOne(e => e.Categoria)
                      .WithMany()
                      .HasForeignKey(e => e.CategoriaId)
                      .IsRequired();

                // Relacionamento Usuario
                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .IsRequired();
            });
            #endregion

            #region Troca
            modelBuilder.Entity<TrocaModel>(entity =>
            {
                entity.ToTable("Troca");

                entity.HasKey(e => e.TrocaId);

                entity.Property(e => e.TrocaStatus)
                      .IsRequired();

                entity.Property(e => e.DataCriacao)
                      .IsRequired();

                entity.HasOne(e => e.ProdutoMeu)
                      .WithMany()
                      .HasForeignKey(e => e.ProdutoIdMeu)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.ProdutoEscolhido)
                      .WithMany()
                      .HasForeignKey(e => e.ProdutoIdEscolhido)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Usuario)
                      .WithMany()
                      .HasForeignKey(e => e.UsuarioId)
                      .IsRequired();
            });
            #endregion
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected DataContext()
        {
        }
    }
}
