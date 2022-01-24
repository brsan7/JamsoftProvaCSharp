#nullable disable
using Microsoft.AspNetCore.Mvc;
using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Dominio;
using API_GerenciamentoProdutos.Repositorio;
using API_GerenciamentoProdutos.Servicos;

namespace API_GerenciamentoProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        public readonly API_GerenciamentoProdutosContexto _context;
        private CompraBLL compraBLL;
        private CompraDAL compraDAL;
        private ProdutoDAL produtoDAL;

        public ComprasController(API_GerenciamentoProdutosContexto context)
        {
            _context = context;
        }

        // POST: api/compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {
            compraBLL = new CompraBLL(_context);
            compraDAL = new CompraDAL(_context);
            produtoDAL = new ProdutoDAL(_context);

            Produto produto = await compraBLL.ValidarDados(compra);

            if (produto == null)
            {
                return StatusCode(412, "Os valores informados não são válidos");
            }

            Pagamento pagamento = new Pagamento
            {
                valor = produto.valor_unitario * compra.qtde_comprada,
                Cartao = compra.Cartao
            };

            //Requisição da rota de pagamentos
            Pagamento validacaoPagamento 
                = await PagamentoService.ValidarPagamento(pagamento);

            if (!validacaoPagamento.estado.Equals("APROVADO"))
            {
                return StatusCode(412, "Os valores informados não são válidos");
            }

            compra.data_compra = DateTime.Now;
            compra.numero_cartao = compra.Cartao.numero_cartao;
            compra.Cartao = null;

            if (await compraDAL.registrar(compra))
            {
                compra.Produto = produto;

                //se houver um erro na atualização do estoque o registro da compra será removido
                if (!await produtoDAL.atualizarEstoque(compra))
                {
                    compra.Produto = null;

                    await compraDAL.reverterCompra(compra);

                    return StatusCode(400, "Ocorreu um erro desconhecido");
                }
            }
            else
            {
                return StatusCode(400, "Ocorreu um erro desconhecido");
            }
            
            return StatusCode(200, "Venda realizada com sucesso");
        }
    }
}
