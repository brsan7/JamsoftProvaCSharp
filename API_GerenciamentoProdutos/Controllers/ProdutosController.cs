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
    public class ProdutosController : ControllerBase
    {
        private readonly JamsoftProvaCSharpContexto _context;

        public ProdutosController(JamsoftProvaCSharpContexto context)
        {
            _context = context;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            Array lstProdutos;

            try
            {
                lstProdutos = await (from produto in _context.Set<Produto>()
                                    select new
                                    {
                                        produto.nome,
                                        produto.valor_unitario,
                                        produto.qtde_estoque
                                    }).ToArrayAsync();
            }
            catch (Exception)
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }

            return Ok(lstProdutos);
        }

        // GET: api/produtos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            Produto produto;

            try
            {
                produto = await _context.Produtos.FindAsync(id);
            }
            catch(Exception)
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }
            
            return Ok(produto);
        }

        // POST: api/produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {

            //adicionar return StatusCode(412, "Os valores informados não são válidos");

            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }

            return StatusCode(200, "Produto Cadastrado");
        }

        // DELETE: api/produtos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            Produto produto;

            try
            {
                produto = await _context.Produtos.FindAsync(id);

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }

            return StatusCode(200, "Produto excluído com sucesso");
        }
    }
}
