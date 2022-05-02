using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using br.com.estoque2017.model;
using Npgsql;

namespace br.com.estoque2017.dao
{
    public class ClienteDAO
    {

        string conexao_postgre = @"Server=127.0.0.1;
                                   Port=5432;
                                   User id=postgres;
                                   Password=jlmfdisk;
                                   Database=estoque";

        NpgsqlConnection conexao;


        public void inserir(Cliente oCliente)
        {
            try
            {
                conexao = new NpgsqlConnection(conexao_postgre);
                NpgsqlCommand sql = 
                  new NpgsqlCommand(
                      "insert into cliente (nomecliente,enderecocliente) " +
                                               "values (@nome,@endereco)", 
                       conexao);

                sql.Parameters.AddWithValue("@nome", oCliente.nomeCliente);
                sql.Parameters.AddWithValue("@endereco", oCliente.enderecoCliente);

                conexao.Open();
                sql.ExecuteNonQuery();
            }
            catch(Exception oErro)
            {
                throw oErro;
            }
            finally
            {
                conexao.Close();
            }
        }

        public void alterar(Cliente oCliente)
        {
            try
            {
                conexao = new NpgsqlConnection(conexao_postgre);
                NpgsqlCommand sql = new NpgsqlCommand("update cliente set nomecliente=@nome, " +
                                                               " enderecocliente = @end " +
                                                      " where idCliente = @id", conexao);
                sql.Parameters.AddWithValue("@id", oCliente.idCliente);
                sql.Parameters.AddWithValue("@nome", oCliente.nomeCliente);
                sql.Parameters.AddWithValue("@end", oCliente.enderecoCliente );

                conexao.Open();
                sql.ExecuteNonQuery();
            }
            catch (Exception oErro)
            {
                throw oErro;
            }
            finally
            {
                conexao.Close();
            }

        }

        public void excluir(Cliente oCliente)
        {
            try
            {
                conexao = new NpgsqlConnection(conexao_postgre);
                NpgsqlCommand sql = new NpgsqlCommand("delete from cliente " +
                                                      "where idcliente = @id", conexao);
                sql.Parameters.AddWithValue("@id", oCliente.idCliente);
                conexao.Open();
                sql.ExecuteNonQuery();
            }
            catch (Exception oErro)
            {
                throw oErro;
            }
            finally
            {
                conexao.Close();
            }


        }

        public Cliente carregar(int idCliente)
        {

            try
            {
                conexao = new NpgsqlConnection(conexao_postgre);
                NpgsqlCommand sql = new NpgsqlCommand("select * from cliente" +
                                               " where idcliente = @id", conexao);
                sql.Parameters.AddWithValue("@id",idCliente);
                conexao.Open();
                Cliente oCliente = new Cliente();
                NpgsqlDataReader datareader;
                datareader = sql.ExecuteReader();
                while (datareader.Read())
                {
                    oCliente.idCliente =
                        Convert.ToInt32(datareader["idcliente"]);
                    oCliente.nomeCliente = datareader["nomecliente"].ToString();
                    oCliente.enderecoCliente = datareader["enderecocliente"].ToString();
                }
                return oCliente;
            }
            catch (NpgsqlException erro)
            {
                throw erro;
            }
            finally
            {
                conexao.Close();
            }

        }

        public List<Cliente> listar()
        {
            try
            {
                conexao = new NpgsqlConnection(conexao_postgre);
                NpgsqlCommand sql = new NpgsqlCommand("select * from cliente", 
                    conexao);
                conexao.Open();
                NpgsqlDataReader dtReader = sql.ExecuteReader();
                var lstCliente = TransformaReaderEmListaDeObjeto(dtReader);
                return lstCliente;
            }
            catch (NpgsqlException erro)
            {
                throw erro;
            }
            finally
            {
                conexao.Close();
            }
        }

        private List<Cliente> TransformaReaderEmListaDeObjeto(
            NpgsqlDataReader retornoDataReader)
        {
            var lstCliente = new List<Cliente>();
            while (retornoDataReader.Read())
            {
                var temObjeto = new Cliente()
                {
                    idCliente = int.Parse(retornoDataReader["idcliente"].ToString()),
                    nomeCliente = retornoDataReader["nomecliente"].ToString(),
                    enderecoCliente = retornoDataReader["enderecocliente"].ToString()
                };
                lstCliente.Add(temObjeto);
            }
            retornoDataReader.Close();
            return lstCliente;
        }



    }
}
