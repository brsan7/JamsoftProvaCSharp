using System;
using System.Collections.Generic;
using System.Text;

namespace ClienteAPI.Models
{
    public class Cartao
    {
        public string titular { get; set; } = String.Empty;

        public string numero_cartao { get; set; } = String.Empty;

        public string data_expiracao { get; set; } = String.Empty;

        public string bandeira { get; set; } = String.Empty;
        public string cvv { get; set; } = String.Empty;
    }
}
