using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Modelo
{
    public class ClienteModelo
    {
        private string nome;
        private string telefone;
        private string cpf;

        public string Nome { get => nome; set => nome = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Cpf { get => cpf; set => cpf = value; }

        public bool SalvarArquivoCliente(string strJson, string caminho)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(caminho))
                {
                    sw.WriteLine(strJson);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
                return false;
            }
        }

       public static string AbrirArquivoCliente(string caminho)
        {
            try
            {
                var strJson = "";

                using (StreamReader sr = new StreamReader(caminho))
                {
                    strJson = sr.ReadToEnd();
                }
                return strJson;
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }
        //para lista
       public bool JsonSerializarLista(List<ClienteModelo> listaCliente, string caminho)
        {
            var strJson = JsonConvert.SerializeObject(listaCliente, Formatting.Indented);
            return SalvarArquivoCliente(strJson, caminho);
        }
       
        public static List<ClienteModelo> JsonDesserializarLista(string caminho)
        {
            var strJson = AbrirArquivoCliente(caminho);
            if (strJson.Substring(0, 4) != "Erro")
            {
                return JsonConvert.DeserializeObject<List<ClienteModelo>>(strJson);
            }
            else
            {
                List<ClienteModelo> listaClientes = new List<ClienteModelo>();
                ClienteModelo cliente = new ClienteModelo();
                cliente.Nome = strJson;
                listaClientes.Add(cliente);
                return listaClientes;
            }
        }
    }
}
