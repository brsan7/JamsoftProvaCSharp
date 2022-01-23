using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_GerenciamentoProdutos.Entidades
{
    public class Cartao
    {
        [Required]
        [MaxLength(50)]
        public string titular { get; set; } = null!;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(17)]
        public string numero_cartao { get; set; } = null!;

        [Required]
        [MaxLength(7)]
        public string data_expiracao { get; set; } = null!;


        [Required]
        [MaxLength(20)]
        public string bandeira { get; set; } = null!;

        [Required]
        [MaxLength(3)]
        public string cvv { get; set; } = null!;
    }
}
