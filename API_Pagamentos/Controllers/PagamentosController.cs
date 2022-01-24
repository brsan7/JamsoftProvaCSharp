#nullable disable
using Microsoft.AspNetCore.Mvc;
using API_GerenciamentoProdutos.Entidades;
using API_Pagamentos.Repositorio;
using API_Pagamentos.Dominio;

namespace API_Pagamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        public readonly API_GerenciamentoProdutosContexto _context;
        private PagamentoDAL pagamentoDAL;

        public PagamentosController(API_GerenciamentoProdutosContexto context)
        {
            _context = context;
        }

        // POST: api/pagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
        {
            pagamentoDAL = new PagamentoDAL(_context);

            Cartao cartao = pagamento.Cartao;
            pagamento.numero_cartao = pagamento.Cartao.numero_cartao;
            pagamento.Cartao = null;

            pagamento.estado = PagamentoBLL.status_compra(pagamento);
            
            if (!await pagamentoDAL.registrar(pagamento))
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }

            pagamento.Cartao = cartao;

            return Ok(pagamento);
        }
    }
}
