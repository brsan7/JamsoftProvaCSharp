using Microsoft.EntityFrameworkCore;
using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Dominio;
using API_GerenciamentoProdutos.Controllers;

namespace API_GerenciamentoProdutos.Repositorio
{
    public class ProdutoDAL : ProdutosController
    {
        public ProdutoDAL(API_GerenciamentoProdutosContexto context) : base(context)
        {
        }

        public async Task<Array?> listarProdutos()
        {
            Array? lstProdutos;

            try
            {
                lstProdutos = await (from produto in _context.Set<Produto>()
                                     select new
                                     {
                                         produto.produto_id,
                                         produto.nome,
                                         produto.valor_unitario,
                                         produto.qtde_estoque
                                     }).ToArrayAsync();
            }
            catch (Exception)
            {
                lstProdutos = null;
            }

            return lstProdutos;
        }
        public async Task<Produto?> buscarId(int id)
        {
            try
            {
                return await _context.Produtos.FindAsync(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Produto>> buscarDuplicidade(string nome)
        {
            List<Produto> resultado;
            try
            {
                resultado = await (from produto in _context.Set<Produto>()
                                   where produto.nome.Contains(nome)
                                   select produto).ToListAsync();
            }
            catch (Exception)
            {
                resultado = new List<Produto>();
            }
            return resultado;
        }

        public async Task<bool> registrar(Produto produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> deletar(int id)
        {
            Produto? produto = await buscarId(id);

            if (produto == null) return false;

            try
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> atualizarEstoque(Compra compra)
        {
            Produto produto = ProdutoBLL.calcularEstoque(compra);

            try
            {
                _context.Produtos.Update(produto);
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
