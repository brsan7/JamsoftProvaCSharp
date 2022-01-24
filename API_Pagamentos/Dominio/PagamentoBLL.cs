using API_GerenciamentoProdutos.Entidades;

namespace API_Pagamentos.Dominio
{
    public class PagamentoBLL
    {
        public static string status_compra(Pagamento pagamento)
        {
            if (pagamento.valor > 100)
            {
                pagamento.estado = "APROVADO";
            }
            else
            {
                pagamento.estado = "REJEITADO";
            }

            return pagamento.estado;
        }
    }
}
