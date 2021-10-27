using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using BancoDeDados;

namespace JSONDAO
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        List<ClienteModelo> listaClientes = new List<ClienteModelo>();

        private void CarregaGridClientes()
        {
            List<ClienteModelo> listaclientes = new ClienteDao().CarregarGridDao();
            dataGridView1.DataSource = listaclientes;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;
        }

        ClienteModelo clienteM = new ClienteModelo();
        ClienteDao clidao = new ClienteDao();
        
        private void btnInserir_Click(object sender, EventArgs e)
        {
            clienteM.Nome = this.txtNome.Text;
            clienteM.Telefone = this.txtTelefone.Text;            
            clienteM.Cpf = this.txtCPF.Text;

            if (clidao.salvar(clienteM))
            {
                MessageBox.Show("Dados salvos com Sucesso!");
                this.txtNome.Text = "";
                this.txtTelefone.Text = "";
                this.txtCPF.Text = "";
                CarregaGridClientes();
            }
            else
            {
                MessageBox.Show("Erro ao salvar os dados!");
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregaGridClientes();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ClienteModelo cliente = new ClienteModelo();
            List<ClienteModelo>  listaclientesGrid = new ClienteDao().CarregarGridDao();
            if (cliente.JsonSerializarLista(listaclientesGrid, @"D:\Json\arquivoListaDao.json"))
            {
                MessageBox.Show("Arquivo Salvo.");
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            listaClientes = ClienteModelo.JsonDesserializarLista(@"D:\Json\arquivoListaDao.json");

            dataGridView1.DataSource = listaClientes;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Lavender;
        }

        private void btnSalvarJsonBanco_Click(object sender, EventArgs e)
        {
            var cliente = new ClienteDao();
            listaClientes = ClienteModelo.JsonDesserializarLista(@"D:\Json\arquivoListaDao.json");

            foreach (var clientes in listaClientes)
            {
                cliente.salvar(clientes);
            }

            MessageBox.Show("Processo Finalizado.");
        }
    }
}
