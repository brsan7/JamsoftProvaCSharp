using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Repositorio;
using API_GerenciamentoProdutos.Controllers;


namespace API_GerenciamentoProdutos.Dominio
{
    public class CartaoBLL : ComprasController
    {
        public CartaoBLL(API_GerenciamentoProdutosContexto context) : base(context)
        {
        }

        public async Task<bool> ValidarDados(Cartao? cartao)
        {
            Cartao? cartao_registrado = await new CartaoDAL(_context).buscarID(cartao!.numero_cartao ?? "");

            if (cartao_registrado == null) return false;
            if (!cartao.titular.Equals(cartao_registrado.titular)) return false;
            if (!cartao.data_expiracao.Equals(cartao_registrado.data_expiracao)) return false;
            if (!cartao.bandeira.Equals(cartao_registrado.bandeira)) return false;
            if (!cartao.cvv.Equals(cartao_registrado.cvv)) return false;

            return true;
        }
    }
}
