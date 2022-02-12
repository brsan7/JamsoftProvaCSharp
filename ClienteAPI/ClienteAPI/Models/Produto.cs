using System;
using System.Collections.Generic;
using System.Text;

namespace ClienteAPI.Models
{
    public class Produto
    {
        public int produto_id { get; set; }
        public string nome { get; set; } = String.Empty;
        public double valor_unitario { get; set; }
        public int qtde_estoque { get; set; }

        public DateTime? data_ultima_compra { get; set; }
        public double? valor_ultima_compra { get; set; }
    }
}
