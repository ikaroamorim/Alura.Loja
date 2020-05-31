using System;
using System.Collections.Generic;
using System.Linq;
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
        var produtos = contexto.Produtos.ToList();
        foreach (var item in produtos)
        {
          Console.WriteLine(item);
        }

        Console.WriteLine("++++++++++++++++++++++");

        foreach (var item in contexto.ChangeTracker.Entries())
        {
          Console.WriteLine(item.State);
        }
        Console.WriteLine("++++++++++++++++++++++\nModificando\n++++++++++++++++++++++");

        var p1 = produtos.First();
        p1.Nome = "Harry poter";

        foreach (var item in contexto.ChangeTracker.Entries())
        {
          Console.WriteLine(item.State);
        }

        Console.WriteLine("++++++++++++++++++++++");

        produtos = contexto.Produtos.ToList();
        foreach (var item in produtos)
        {
          Console.WriteLine(item);
        }
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
      using(var repo = new ProdutoDAOEntity())
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
    }

    private static void GravarUsandoAdoNet()
    {
      Produto p = new Produto();
      p.Nome = "Harry Potter e a Ordem da Fênix";
      p.Categoria = "Livros";
      p.Preco = 19.89;

      using (var repo = new ProdutoDAO())
      {
        repo.Adicionar(p);
      }
    }
  }
}
