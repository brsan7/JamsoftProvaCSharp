using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamsoftProvaCSharp.Entidades
{
    public class Produto
    {
        [Key]
        public int produto_id { get; set; }

        [Required]
        [MaxLength(100)]
        public string nome { get; set; } = null!;

        [Required]
        [Column("valor_unitario", TypeName = "smallmoney")]
        public double valor_unitario { get; set; }

        [Required]
        public int qtde_estoque { get; set;}
    }
}
