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
    }

    private static void GravarUsandoEntity()
    {
      Produto p1 = new Produto();
      p1.Nome = "Harry Potter e a Ordem da Fênix";
      p1.Categoria = "Livros";
      p1.Preco = 19.89;

      Produto p2 = new Produto();
      p2.Nome = "Senhor dos Anéis 1";
      p2.Categoria = "Livros";
      p2.Preco = 19.89;

      Produto p3 = new Produto();
      p3.Nome = "O Monge e o Executivo";
      p3.Categoria = "Livros";
      p3.Preco = 19.89;

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
