using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Modelo;

namespace BancoDeDados
{
    public class ClienteDao
    {
        public ClienteDao()
        {
            DB conexaoDao = new DB();
        }
        //metodo para inserir clientes no banco
        public Boolean salvar(ClienteModelo cliM)
        {
            //abrir a conexao
            SqlConnection conn = DB.abrirConexao();
            //string para inserção
            string sql = "INSERT INTO clientes (cpf, nome, telefone) VALUES (@cpf,@nome,@telefone)";
            try
            {
                //criar um objeto passando a conexao e a sql inserção
                SqlCommand comando = new SqlCommand(sql, conn);
                //adicionando os valores a sql
                comando.Parameters.AddWithValue("@nome", cliM.Nome);
                comando.Parameters.AddWithValue("@telefone", cliM.Telefone);
                comando.Parameters.AddWithValue("@cpf", cliM.Cpf);
                //abrir a conexao
                DB.abrirConexao();
                //executar os comandos
                comando.ExecuteNonQuery();
                return true;
            }
            catch (SqlException erro)
            {
                Console.WriteLine("Erro ao inserir dados no banco" + erro);
                return false;
            }
            finally
            {
                DB.fecharConexao();
            }

        }
        //metodo para carregar a datagridview
        public List<ClienteModelo> CarregarGridDao()
        {
            List<ClienteModelo> listaclientes = new List<ClienteModelo>();
            ClienteModelo clientes = null;

            String sql = "SELECT nome, telefone, cpf FROM clientes";

            SqlConnection conn = DB.abrirConexao();
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    clientes = new ClienteModelo();

                    if (dr["nome"] != DBNull.Value)
                        clientes.Nome = (dr["nome"].ToString());

                    if (dr["telefone"] != DBNull.Value)
                        clientes.Telefone = (dr["telefone"].ToString());

                    if (dr["cpf"] != DBNull.Value)
                        clientes.Cpf = dr["cpf"].ToString();

                   if (listaclientes == null)
                        listaclientes = new List<ClienteModelo>();

                    listaclientes.Add(clientes);
                }

                return listaclientes;
            }
            catch (SqlException erro)
            {
                Console.WriteLine("Erro ao pesquisar dados no banco" + erro);
                return listaclientes;
            }
            finally
            {
                DB.fecharConexao();
            }
        }        
    }
}
