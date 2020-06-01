using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
  internal class LojaContext : DbContext, IDisposable
  {
    public DbSet<Produto> Produtos { get; set; } //Nome da tabela do banco de dados
    public DbSet<Compra> Compras { get; set; }
    public DbSet<Promocao> Promocoes { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; } //Ess propriedade é opcional pois ao mapear clientes ele cria a tabela correspondente

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .Entity<PromocaoProduto>()
        .HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });

      modelBuilder
        .Entity<Endereco>()
        .Property<int>("ClienteId");

      modelBuilder
        .Entity<Endereco>()
        .HasKey("ClienteId");

      /*  ---- Seria necessário se não tivessemos criado a prop, pois mapearia a tabela errado
      modelBuilder
        .Entity<Endereco>()
        .ToTable("Enderecos");
      */
      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
    }
  }
}

/*Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = LojaDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False*/