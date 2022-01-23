using API_GerenciamentoProdutos.Entidades;
using API_GerenciamentoProdutos.Controllers;

namespace API_GerenciamentoProdutos.Repositorio
{
    public class CompraDAL : ComprasController
    {
        public CompraDAL(API_GerenciamentoProdutosContexto context) : base(context)
        {
        }

        public async Task<bool> registrar(Compra compra)
        {
            try
            {
                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> reverterCompra(Compra compra)
        {
            try
            {
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //
            }
            return false;
        }
    }
}
