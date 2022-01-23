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
            Produto? produto = await new ProdutoDAL(_context).buscarId(compra.produto_id);

            if (!ProdutoBLL.ValidarDados(produto))
            {
                produto = null;
            }

            if (!await new CartaoBLL(_context).ValidarDados(compra.Cartao))
            {
                produto = null;
            }

            return produto;
        }
    }
}
