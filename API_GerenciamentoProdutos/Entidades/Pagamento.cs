using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_GerenciamentoProdutos.Entidades
{
    public class Pagamento
    {
        [Key]
        public int pagamento_id { get; set; }

        [ForeignKey("Cartao")]
        [MaxLength(17)]
        public string numero_cartao { get; set; } = "";

        [Column("valor", TypeName = "money")]
        public double valor { get; set; }

        [MaxLength(10)]
        public string status_compra { get; set; } = "";


        //Constraint chave estrangeira FK_Pagamentos_Cartoes_numero_cartao
        public Cartao? Cartao { get; set; } = null;
    }
}
