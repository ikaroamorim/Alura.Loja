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
      //GravarUsandoAdoNet();
      GravarUsandoEntity();
      RecuperarProdutos();
      //ExcluirProdutos();
      //RecuperarProdutos();
      AtualizarProduto();
      RecuperarProdutos();
    }

    private static void AtualizarProduto()
    {

      using (var repo = new LojaContext())
      {
        Produto primeiro = repo.Produtos.First();
        primeiro.Nome = "Cassino Royale - Editado";
        repo.SaveChanges();
      }
    }

    private static void ExcluirProdutos()
    {
      using(var repo = new LojaContext())
      {
        IList<Produto> produtos = repo.Produtos.ToList();
        foreach (var item in produtos)
        {
          repo.Produtos.Remove(item);
        }
        repo.SaveChanges();
      }
    }

    private static void RecuperarProdutos()
    {
      using (var repo = new LojaContext())
      {
        IList<Produto> produtos = repo.Produtos.ToList();
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

      using (var contexto = new LojaContext())
      {
        //contexto.Produtos.Add(p1);
        //contexto.Produtos.Add(p2);
        //contexto.Produtos.Add(p3);
        contexto.AddRange(p1, p2, p3);
        contexto.SaveChanges();
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
