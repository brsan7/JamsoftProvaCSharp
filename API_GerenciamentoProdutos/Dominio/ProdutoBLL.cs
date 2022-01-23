using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Controllers;


namespace API_GerenciamentoProdutos.Dominio
{
    public class ProdutoBLL : ProdutosController
    {
        public ProdutoBLL(API_GerenciamentoProdutosContexto context) : base(context)
        {
        }

        public static bool ValidarDados(Produto? produto)
        {
            if (produto == null) return false;
            if (produto.nome == null) return false;
            if (produto.nome.Equals("")) return false;
            if (produto.qtde_estoque <= 0) return false;
            if (produto.valor_unitario <= 0) return false;

            return true;
        }

        public static Produto calcularEstoque(Compra compra)
        {
            compra.Produto!.qtde_estoque -= compra.qtde_comprada;
            compra.Produto.data_ultima_compra = compra.data_compra;
            compra.Produto.valor_ultima_compra = compra.Produto.valor_unitario * compra.qtde_comprada;

            Produto produto = compra.Produto;

            return produto;
        }
    }
}
