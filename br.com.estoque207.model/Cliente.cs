using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.com.estoque2017.model
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public String nomeCliente { get; set; }
        public String enderecoCliente { get; set; }

        public Cliente() { }

        public String listarCliente()
        {
            String retornoCliente;

            retornoCliente = "Id :" + this.idCliente +
                             "Nome :" + this.nomeCliente +
                             "Endereço : " + this.enderecoCliente;

            return retornoCliente;
        }

    }
}
