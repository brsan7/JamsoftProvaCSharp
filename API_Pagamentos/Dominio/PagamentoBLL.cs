using API_GerenciamentoProdutos.Entidades;

namespace API_Pagamentos.Dominio
{
    public class PagamentoBLL
    {
        public static string status_compra(Pagamento pagamento)
        {
            if (pagamento.valor > 100)
            {
                pagamento.status_compra = "APROVADO";
            }
            else
            {
                pagamento.status_compra = "REJEITADO";
            }

            return pagamento.status_compra;
        }
    }
}
