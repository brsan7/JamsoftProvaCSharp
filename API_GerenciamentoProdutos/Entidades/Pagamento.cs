using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JamsoftProvaCSharp.Entidades
{
    public class Pagamento
    {
        [Key]
        public int pagamento_id { get; set; }

        
        [Required]
        [MaxLength(50)]
        public string titular { get; set; } = null!;

        [Required]
        [Column("numero_cartao", TypeName = "bigint")]
        public Int64 numero_cartao { get; set; }

        [Required]
        public DateTime data_expiracao { get; set; }

        
        [Required]
        [MaxLength(20)]
        public string bandeira { get; set; } = null!;

        [Required]
        [Column("cvv", TypeName = "smallint")]
        public int cvv { get; set; }
    }
}
