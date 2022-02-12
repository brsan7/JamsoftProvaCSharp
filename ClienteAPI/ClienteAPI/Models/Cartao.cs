using System;
using System.Collections.Generic;
using System.Text;

namespace ClienteAPI.Models
{
    public class Cartao
    {
        public string titular { get; set; } = "John Doe";

        public string numero_cartao { get; set; } = "4111111111111111";

        public string data_expiracao { get; set; } = "12/2018";

        public string bandeira { get; set; } = "VISA";
        public string cvv { get; set; } = "123";
    }
}
