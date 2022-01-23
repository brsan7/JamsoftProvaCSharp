#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using API_GerenciamentoProdutos.Entidades;

namespace API_GerenciamentoProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly API_GerenciamentoProdutosContexto _context;

        public ComprasController(API_GerenciamentoProdutosContexto context)
        {
            _context = context;
        }

        // POST: api/compras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {
            Produto produto = await buscarProduto(compra.produto_id);

            if (!await ValidarDados(produto, compra))
            {
                ///////////ANALIZAR A NECESSIDADE DE MAIS VERIFICAÇÕES///////////
                return StatusCode(412, "Os valores informados não são válidos");
            }

            Pagamento pagamento = new Pagamento
            {
                valor = produto.valor_unitario * compra.qtde_comprada,
                Cartao = compra.Cartao
            };

            //Requisição da rota de pagamentos
            Pagamento validacaoPagamento = await ValidarPagamentoAsync(pagamento);
            
            if (validacaoPagamento.status_compra.Equals("APROVADO"))
            {
                compra.data_compra = DateTime.Now;
                compra.numero_cartao = compra.Cartao.numero_cartao;
                compra.Cartao = null;

                try
                {
                    _context.Compras.Add(compra);
                    await _context.SaveChangesAsync();

                    if (!await atualizarEstoque(produto,compra))
                    {
                        return StatusCode(400, "Ocorreu um erro desconhecido");
                    }
                }
                catch (Exception)
                {
                    return StatusCode(400, "Ocorreu um erro desconhecido");
                }

                return StatusCode(200, "Venda realizada com sucesso");
            }

            return StatusCode(412, "Os valores informados não são válidos");
        }

        private async Task<bool> ValidarDados(Produto produto, Compra compra)
        {
            Cartao cartao_registrado;
            try
            {
                cartao_registrado = await _context.Cartoes.FindAsync(compra.Cartao.numero_cartao);
            }
            catch (Exception)
            {
                return false;
            }

            if (!compra.Cartao.titular.Equals(cartao_registrado.titular)) return false;
            if (!compra.Cartao.data_expiracao.Equals(cartao_registrado.data_expiracao)) return false;
            if (!compra.Cartao.bandeira.Equals(cartao_registrado.bandeira)) return false;
            if (!compra.Cartao.cvv.Equals(cartao_registrado.cvv)) return false;

            if (produto == null) return false;
            if (produto.qtde_estoque < compra.qtde_comprada) return false;

            return true;
        }

        private async Task<Produto> buscarProduto(int id)
        {
            Produto produto;

            try
            {
                produto = await _context.Produtos.FindAsync(id);
            }
            catch (Exception)
            {
                produto = null;
            }

            return produto;
        }

        private async Task<bool> atualizarEstoque(Produto produto, Compra compra)
        {
            try
            {
                produto.qtde_estoque -= compra.qtde_comprada;
                produto.data_ultima_compra = compra.data_compra;
                produto.valor_ultima_compra = produto.valor_unitario * compra.qtde_comprada;

                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //Em caso de erro na atualização do estoque o registro de compra é removido
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();

                return false;
            }

            return true;
        }

        static async Task<Pagamento> ValidarPagamentoAsync(Pagamento pagamento)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync(
                                                        "api/pagamentos", pagamento);
            response.EnsureSuccessStatusCode();

            var formatters = new List<MediaTypeFormatter>() {
                                                        new JsonMediaTypeFormatter(),
                                                        new XmlMediaTypeFormatter() };

            return await response.Content.ReadAsAsync<Pagamento>(formatters);
        }
    }
}
