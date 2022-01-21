#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JamsoftProvaCSharp.Entidades;

namespace JamsoftProvaCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly JamsoftProvaCSharpContexto _context;

        public ComprasController(JamsoftProvaCSharpContexto context)
        {
            _context = context;
        }

        // POST: api/compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {

            if (!CartaoExists(compra.Cartao.numero_cartao))
            {
                //////AMPLIAR return StatusCode(412, "Os valores informados não são válidos");
                return StatusCode(412, "Os valores informados não são válidos");
            }

            //////VALIDAR TRANSAÇÃO//////

            compra.data_compra = DateTime.Now;
            compra.numero_cartao = compra.Cartao.numero_cartao;
            compra.Cartao = null;

            try
            {
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }
            
            return StatusCode(200, "Venda realizada com sucesso");
        }

        private bool CartaoExists(string id_numero_cartao)
        {
            return _context.Cartoes.Any(e => e.numero_cartao.Equals(id_numero_cartao));
        }
    }
}
