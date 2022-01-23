using API_GerenciamentoProdutos.Entidades;
using API_Pagamentos.Controllers;

namespace API_Pagamentos.Repositorio
{
    public class PagamentoDAL : PagamentosController
    {
        public PagamentoDAL(API_GerenciamentoProdutosContexto context) : base(context)
        {
        }

        public async Task<bool> registrar(Pagamento pagamento)
        {
            try
            {
                _context.Pagamentos.Add(pagamento);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
