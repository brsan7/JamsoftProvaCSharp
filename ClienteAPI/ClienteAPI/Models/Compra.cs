using System;
using System.Collections.Generic;
using System.Text;

namespace ClienteAPI.Models
{
    public class Compra
    {
        public int compra_id { get; set; }

        public int produto_id { get; set; }

        public string numero_cartao { get; set; } = "";

        public int qtde_comprada { get; set; }

        public DateTime data_compra { get; set; }



        //Constraint chave estrangeira FK_Compras_Produtos_produto_id
        public Produto Produto { get; set; } = null;

        //Constraint chave estrangeira FK_Compras_Cartoes_numero_cartao
        public Cartao Cartao { get; set; } = null;
    }
}
