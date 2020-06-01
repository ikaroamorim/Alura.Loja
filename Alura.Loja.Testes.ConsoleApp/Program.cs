using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var contexto = new LojaContext())
      {
        var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(SqlLoggerProvider.Create());

        var cliente = contexto
          .Clientes
          .Include(c => c.EnderecoEntrega)
          .FirstOrDefault();

        var produto = contexto
          .Produtos
          .Include(p => p.Compras)
          .Where(p => p.Id == 9004)
          .FirstOrDefault();

        //Console.WriteLine("Mostrando as compras do"+ produto.Nome);
        foreach (var item in produto.Compras)
        {
          Console.WriteLine(item);
        }

        Console.WriteLine($"Endereço de Entrega: {cliente.EnderecoEntrega.Logradouro}");
      }
    }

    private static void ExibeProdutosPromo()
    {
      using (var contexto2 = new LojaContext())
      {
        var serviceProvider = contexto2.GetInfrastructure<IServiceProvider>();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(SqlLoggerProvider.Create());

        var promocao = contexto2
          .Promocoes
          .Include(p => p.Produtos)
          .ThenInclude(pp => pp.Produto)
          .FirstOrDefault();

        Console.WriteLine("\nMostrando os produtos da promoção...");

        foreach (var item in promocao.Produtos)
        {
          Console.WriteLine(item.Produto);
        }
      }
    }

    private static void IncluirPromocao()
    {
      using (var contexto = new LojaContext())
      {
        var promocao = new Promocao();
        promocao.Descricao = "Queima total Jan/2017";
        promocao.DataInicio = new DateTime(2020, 05, 31);
        promocao.DataTermino = new DateTime(2020, 08, 31);

        var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebidas").ToList();

        foreach (var item in produtos)
        {
          promocao.IncluiProduto(item);
        }

        contexto.Promocoes.Add(promocao);

        Exibeentries(contexto.ChangeTracker.Entries());

        contexto.SaveChanges();
      }
    }

    private static void UmPraUm()
    {
      var fulano = new Cliente();
      fulano.Nome = "Fulano Silva";

      fulano.EnderecoEntrega = new Endereco()
      {
        Logradouro = "Rua dos Inválidos",
        Numero = 12,
        Complemento = "Sobrado",
        Bairro = "Centro",
        Cidade = "São Paulo"
      };

      using (var contexto = new LojaContext())
      {
        contexto.Clientes.Add(fulano);
        contexto.SaveChanges();
      }
    }

    private static void UmMuitosEMuitosMuitos()
    {
      var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.7, Unidade = "Litros" };
      var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 11.7, Unidade = "Kilo" };
      var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimento", PrecoUnitario = 3.47, Unidade = "Pacote" };
      var promocaoPascoa = new Promocao();
      promocaoPascoa.Descricao = "Páscoa Feliz";
      promocaoPascoa.DataInicio = DateTime.Now;
      promocaoPascoa.DataTermino = DateTime.Now.AddMonths(3);
      promocaoPascoa.IncluiProduto(p1);
      promocaoPascoa.IncluiProduto(p2);
      promocaoPascoa.IncluiProduto(p3);

      using (var contexto = new LojaContext())
      {
        var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(SqlLoggerProvider.Create());

        //contexto.Promocoes.Add(promocaoPascoa);
        //Exibeentries(contexto.ChangeTracker.Entries());
        //contexto.SaveChanges();

        var promocao = contexto.Promocoes.First();
        contexto.Promocoes.Remove(promocao);
        Exibeentries(contexto.ChangeTracker.Entries());
        contexto.SaveChanges();

      }


      var paoFrances = new Produto()
      {
        Nome = "Pão Frances",
        PrecoUnitario = 0.40,
        Unidade = "Unidade",
        Categoria = "Padaria"
      };

      var compra = new Compra();
      compra.Quantidade = 6;
      compra.Produto = paoFrances;
      compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

      using (var contexto = new LojaContext())
      {
        var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(SqlLoggerProvider.Create());

        contexto.Compras.Add(compra);
        Exibeentries(contexto.ChangeTracker.Entries());

        contexto.SaveChanges();
      }
    }

    private static void ExplicaoChangeTracker()
    {
      using (var contexto = new LojaContext())
      {
        var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        loggerFactory.AddProvider(SqlLoggerProvider.Create());

        var produtos = contexto.Produtos.ToList();

        Exibeentries(contexto.ChangeTracker.Entries());

        Console.WriteLine("++++++++++++++++++++++\nModificando\n++++++++++++++++++++++");

        //Alteração de um produto
        //var p1 = produtos.First();
        //p1.Nome = "Harry poter e a Ordem da Fenix";

        //Inclusão de novo Produto
        //var novoProduto = new Produto()
        //{
        //  Nome = "Desinfetante",
        //  Categoria = "Limpeza",
        //  Preco = 2.99
        //};
        //contexto.Produtos.Add(novoProduto);

        //Exclusão de produto
        var p1 = produtos.Last();
        contexto.Produtos.Remove(p1);

        Exibeentries(contexto.ChangeTracker.Entries());

        contexto.SaveChanges();

        Exibeentries(contexto.ChangeTracker.Entries());
      }
    }

    private static void Exibeentries(IEnumerable<EntityEntry> entries)
    {
      Console.WriteLine("++++++++++++++++++++++");

      foreach (var item in entries)
      {
        Console.WriteLine(item.Entity.ToString() + " - " + item.State);
      }

    }

    private static void AtualizarProduto()
    {

      using (var repo = new ProdutoDAOEntity())
      {
        var primeiro = repo.Produtos().First();
        primeiro.Nome = "Cassino Royale - Editado";
        repo.Atualizar(primeiro);
      }
    }

    private static void ExcluirProdutos()
    {
      using (var repo = new ProdutoDAOEntity())
      {
        IList<Produto> produtos = repo.Produtos();
        foreach (var item in produtos)
        {
          repo.Remover(item);
        }
      }
    }

    private static void RecuperarProdutos()
    {
      using (var repo = new ProdutoDAOEntity())
      {
        IList<Produto> produtos = repo.Produtos();
        Console.WriteLine($"Foram encontrados: {produtos.Count} produtos");
        foreach (var item in produtos)
        {
          Console.WriteLine(item.Nome);
        }
      }
    }

    private static void GravarUsandoEntity()
    {
      /*
      Produto p1 = new Produto();
      p1.Nome = "O Carteiro e o Poeta";
      p1.Categoria = "Livros";
      p1.Preco = 13.99;

      Produto p2 = new Produto();
      p2.Nome = "Senhor dos Anéis 1";
      p2.Categoria = "Livros";
      p2.Preco = 19.89;

      Produto p3 = new Produto();
      p3.Nome = "O Monge e o Executivo";
      p3.Categoria = "Livros";
      p3.Preco = 25.49;

      using (var contexto = new ProdutoDAOEntity())
      {
        //contexto.Adicionar(p1, p2, p3);
        contexto.Adicionar(p1);
        contexto.Adicionar(p2);
        contexto.Adicionar(p3);
      }
      */
    }

    private static void GravarUsandoAdoNet()
    {
      /*Produto p = new Produto();
      p.Nome = "Harry Potter e a Ordem da Fênix";
      p.Categoria = "Livros";
      p.Preco = 19.89;

      using (var repo = new ProdutoDAO())
      {
        repo.Adicionar(p);
      }
      */
    }
  }
}
