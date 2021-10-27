using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BancoDeDados
{
    class DB
    {
        //conectando com o bando sqlserver express à base de dados usando whindow authentication
        private static string connString = @"Server = MEEY\SQLEXPRESS; Database = bancojson; Trusted_Connection = true";
        //criar um atributo para a conexao
        private static SqlConnection conn = null;
        //metodo para realizar a conexao
        public static SqlConnection abrirConexao()
        {
            //criar a conexao
            conn = new SqlConnection(connString);
            //conexao ok
            try
            {
                //abrir a conexão
                conn.Open();
            }
            catch (SqlException sqle)
            {
                conn = null;
                Console.WriteLine("Erro ao Realizar a conexao!" + sqle);
            }
            return conn;
        }
        public static void fecharConexao()
        {
            if (conn != null)
            {
                try
                {
                    conn.Close();
                }
                catch (SqlException sqle)
                {
                    Console.WriteLine("Erro ao Realizar a conexao!" + sqle);
                }
            }
        }
    }
}
