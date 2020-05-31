namespace Alura.Loja.Testes.ConsoleApp
{
  internal class Compra
  {
    public int Id { get; set; }
    public int Quantidade { get; internal set; }
    public int ProdutoId { get; set; } //Inserida essa coluna pois, caso contrário, o Entity atribuiria a coluna Produto Id a característica nullable
    public Produto Produto { get; internal set; }
    public double Preco { get; internal set; }
  }
}