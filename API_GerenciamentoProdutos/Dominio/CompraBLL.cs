using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Repositorio;
using API_GerenciamentoProdutos.Controllers;

namespace API_GerenciamentoProdutos.Dominio
{
    public class CompraBLL : ComprasController
    {
        public CompraBLL(API_GerenciamentoProdutosContexto context) : base(context)
        {
        }

        public async Task<Produto?> ValidarDados(Compra compra)
        {
            ProdutoBLL produtoBLL = new ProdutoBLL(_context);
            Produto? produto = await new ProdutoDAL(_context).buscarId(compra.produto_id);

            if (!ProdutoBLL.ValidarDados(produto))
            {
                produto = null;
            }

            if (!await new CartaoBLL(_context).ValidarDados(compra.Cartao))
            {
                produto = null;
            }

            if (produto != null && compra.qtde_comprada > produto.qtde_estoque)
            {
                produto = null;
            }

            if (compra.qtde_comprada <= 0) produto = null;

            return produto;
        }
    }
}
