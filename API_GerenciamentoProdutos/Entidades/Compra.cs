using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_GerenciamentoProdutos.Entidades
{
    public class Compra
    {
        [Key]
        public int compra_id { get; set; }

        [ForeignKey("Produto")]
        public int produto_id { get; set; }

        [ForeignKey("Cartao")]
        [MaxLength(17)]
        public string numero_cartao { get; set; } = "";

        [Required]
        public int qtde_comprada { get; set; }

        [Required]
        public DateTime data_compra { get; set; }



        //Constraint chave estrangeira FK_Compras_Produtos_produto_id
        public Produto? Produto { get; set; } = null;

        //Constraint chave estrangeira FK_Compras_Cartoes_numero_cartao
        public Cartao? Cartao { get; set; } = null;
    }
}
