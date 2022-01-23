using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Controllers;

namespace API_GerenciamentoProdutos.Repositorio
{
    public class CartaoDAL : ComprasController
    {
        public CartaoDAL(API_GerenciamentoProdutosContexto context) : base(context)
        {
        }

        public async Task<Cartao?> buscarID(string id)
        {
            try
            {
                return await _context.Cartoes.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
