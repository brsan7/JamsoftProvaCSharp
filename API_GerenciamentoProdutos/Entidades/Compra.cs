using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamsoftProvaCSharp.Entidades
{
    public class Compra
    {
        [Key]
        public int compra_id { get; set; }

        [ForeignKey("Produto")]
        public int produto_id { get; set; }

        [ForeignKey("Pagamento")]
        public int pagamento_id { get; set; }

        [Required]
        public int qtde_comprada { get; set; }

        [Required]
        [Column("valor_compra", TypeName = "money")]
        public double valor_compra { get; set; }

        [Required]
        public DateTime data_compra { get; set; }



        //necessário para a criação da chave estrangeira FK_Compras_Produtos_produto_id
        public Produto Produto { get; set; } = null!;

        //necessário para a criação da chave estrangeira FK_Compras_Pagamentos_pagamento_id
        public Pagamento Pagamento { get; set; } = null!;
    }
}
