#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_GerenciamentoProdutos.Entidades;

namespace API_Pagamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly API_GerenciamentoProdutosContexto _context;

        public PagamentosController(API_GerenciamentoProdutosContexto context)
        {
            _context = context;
        }

        // POST: api/Pagamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
        {
            pagamento.numero_cartao = pagamento.Cartao.numero_cartao;
            Cartao cartao = pagamento.Cartao;
            pagamento.Cartao = null;

            if (pagamento.valor > 100)
            {
                pagamento.status_compra = "APROVADO";
            }
            else
            {
                pagamento.status_compra = "REJEITADO";
            }

            try
            {
                _context.Pagamentos.Add(pagamento);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }

            pagamento.Cartao = cartao;

            return pagamento;
        }
    }
}
