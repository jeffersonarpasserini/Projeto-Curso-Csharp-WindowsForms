using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using br.com.estoque2017.model;
using br.com.estoque2017.dao;

namespace br.com.estoque2017.aplicacao
{
    public class ClienteApp
    {

        public ClienteApp() { }

        ClienteDAO oClienteDAO;

        public void inserir(Cliente oCliente)
        {
           try
            {
                if (String.IsNullOrEmpty(oCliente.nomeCliente))
                {
                    Exception oErroNome = 
                        new Exception("Preencher corretamente o nome do cliente");
                    throw oErroNome;
                }

                oClienteDAO = new ClienteDAO();

                oClienteDAO.inserir(oCliente);

            }
            catch(Exception oErro)
            {
                throw oErro;
            }

        }

        public void alterar(Cliente oCliente)
        {
            try
            {
                if (String.IsNullOrEmpty(oCliente.nomeCliente))
                {
                    Exception oErroNome =
        new Exception("Preencher corretamente o nome do cliente");
                    throw oErroNome;
                }

                oClienteDAO = new ClienteDAO();

                oClienteDAO.alterar(oCliente);

            }
            catch (Exception oErro)
            {
                throw oErro;
            }


        }

        public void excluir(Cliente oCliente)
        {
            try
            {
                oClienteDAO = new ClienteDAO();
                oClienteDAO.excluir(oCliente);
            }
            catch (Exception oErro)
            {
                throw oErro;
            }
        }

        public List<Cliente> listar()
        {
            try
            {
                oClienteDAO = new ClienteDAO();
                return oClienteDAO.listar();
            }
            catch (Exception oErro)
            {
                throw oErro;
            }

        }

        public Cliente carregar(Cliente oCliente)
        {
            try
            {
                oClienteDAO = new ClienteDAO();
                oCliente = oClienteDAO.carregar(oCliente.idCliente);
            }
            catch (Exception oErro)
            {
                throw oErro;
            }
            return oCliente;
        }

    }
}
