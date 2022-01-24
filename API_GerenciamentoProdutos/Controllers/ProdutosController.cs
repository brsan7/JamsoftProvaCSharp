#nullable disable
using Microsoft.AspNetCore.Mvc;
using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Dominio;
using API_GerenciamentoProdutos.Repositorio;

namespace API_GerenciamentoProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        public readonly API_GerenciamentoProdutosContexto _context;
        private ProdutoDAL produtoDAL;
        private ProdutoBLL produtoBLL;

        public ProdutosController(API_GerenciamentoProdutosContexto context)
        {
            _context = context;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            produtoDAL = new ProdutoDAL(_context);

            Array lstProdutos = await produtoDAL.listarProdutos();

            if (lstProdutos == null)
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }

            return Ok(lstProdutos);
        }

        // GET: api/produtos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            produtoDAL = new ProdutoDAL(_context);

            Produto produto = await produtoDAL.buscarId(id);

            if (produto == null)
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
            produtoDAL = new ProdutoDAL(_context);
            produtoBLL = new ProdutoBLL(_context);

            if (!ProdutoBLL.ValidarDados(produto) || !await produtoBLL.ValidarUnicidade(produto))
            {
                return StatusCode(412, "Os valores informados não são válidos");
            }

            if (!await produtoDAL.registrar(produto))
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }
            
            return StatusCode(200, "Produto Cadastrado");
        }

        // DELETE: api/produtos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            produtoDAL = new ProdutoDAL(_context);

            if (!await produtoDAL.deletar(id))
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }
            
            return StatusCode(200, "Produto excluído com sucesso");
        }
    }
}
