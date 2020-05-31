﻿using Microsoft.EntityFrameworkCore;
using System;

namespace Alura.Loja.Testes.ConsoleApp
{
  internal class LojaContext : DbContext, IDisposable
  {
    public DbSet<Produto> Produtos { get; set; } //Nome da tabela do banco de dados
    public DbSet<Compra> Compras { get; set; }
    public DbSet<Promocao> Promocoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder
        .Entity<PromocaoProduto>()
        .HasKey(pp => new { pp.PromocaoId, pp.ProdutoId });
      base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
    }
  }
}

/*Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = LojaDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False*/