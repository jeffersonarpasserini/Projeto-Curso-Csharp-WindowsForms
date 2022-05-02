using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using br.com.estoque2017.model;
using br.com.estoque2017.aplicacao;

namespace estoque2017
{
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        public void ativaCancelar()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
            btnSair.Enabled = true;
        }

        public void ativaInclusao()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
            btnSair.Enabled = true;

            txtIdCliente.Enabled = false;
            txtNomeCliente.Focus();
        }

        public void ativaAlteracao()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;
            btnSair.Enabled = true;

            txtIdCliente.Enabled = false;
            txtNomeCliente.Focus();

        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            ativaCancelar();
            montaGrid();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            ativaInclusao();
            txtIdCliente.Enabled = false;
            txtIdCliente.Text = "0";
            txtNomeCliente.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var oCliente = montaCliente();
                ClienteApp oClienteApp = new ClienteApp();
                oClienteApp.inserir(oCliente);
                MessageBox.Show("Cliente Gravado com sucesso!",
        "Inclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpaTela();
                montaGrid();
                txtNomeCliente.Focus();
            }
            catch(Exception oErro)
            {
                MessageBox.Show(oErro.Message, "Erro", 
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Cliente montaCliente()
        {
            Cliente oCliente = new Cliente();
            oCliente.idCliente = Convert.ToInt32(txtIdCliente.Text);
            oCliente.enderecoCliente = txtEnderecoCliente.Text;
            oCliente.nomeCliente = txtNomeCliente.Text;
            return oCliente;
        }

        private void limpaTela()
        {
            txtEnderecoCliente.Text = "";
            txtNomeCliente.Text = "";
            txtIdCliente.Text = "0";
        }

        private void montaGrid()
        {
            try
            {
                grdCliente.Rows.Clear();
                ClienteApp oClienteRN = new ClienteApp();
                foreach (Cliente oCliente in oClienteRN.listar())
                {
                    grdCliente.Rows.Add(oCliente.idCliente.ToString(),
                                       oCliente.nomeCliente);
                }
            }
            catch (Exception oErro)
            {
                MessageBox.Show("Erro :" + oErro.Message);
            }
        }

        private void grdCliente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtIdCliente.Enabled = true;
                txtIdCliente.Text = grdCliente.Rows[grdCliente.CurrentRow.Index].Cells["idcliente"].Value.ToString();
                txtIdCliente_Validating(null, null);
            }
            catch (Exception oErro)
            {
                MessageBox.Show("Erro :" + oErro.Message);
            }
        }

        private void txtIdCliente_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtIdCliente.Text))
                {
                    ClienteApp oClienteApp = new ClienteApp();
                    var oCliente = oClienteApp.carregar(montaCliente());

                    if (String.IsNullOrEmpty(oCliente.nomeCliente))
                    {
                        MessageBox.Show("Cliente não encontrada");
                    }

                    carregaDados(oCliente);
                    ativaAlteracao();
                }

            }
            catch (Exception oErro)
            {
                MessageBox.Show("Erro :" + oErro.Message);
            }
        }

        private void frmCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");

            if (e.KeyCode == Keys.F2)
            {
                ativaInclusao();
                limpaTela();
            }

            if (e.KeyCode == Keys.F3)
                //grava a inclusão em um objeto
                btnSalvar_Click(null, null);

            if (e.KeyCode == Keys.F4)
            {
                btnAlterar_Click(null, null);
            }

            if (e.KeyCode == Keys.F5)
            {
                btnExcluir_Click(null, null);
            }

            if (e.KeyCode == Keys.F12)
                ativaCancelar();
                
        }

        private void carregaDados(Cliente oCliente)
        {
            try
            {
                txtIdCliente.Text = oCliente.idCliente.ToString();
                txtNomeCliente.Text = oCliente.nomeCliente;
                txtEnderecoCliente.Text = oCliente.enderecoCliente;
            }
            catch (Exception oErro)
            {
                MessageBox.Show("Erro :" + oErro.Message);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                var oCliente = montaCliente();
                ClienteApp oClienteApp = new ClienteApp();
                oClienteApp.alterar(oCliente);
                MessageBox.Show("Cliente alterado com sucesso!",
        "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpaTela();
                montaGrid();
                txtNomeCliente.Focus();
            }
            catch (Exception oErro)
            {
                MessageBox.Show(oErro.Message, "Erro",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                var oCliente = montaCliente();
                ClienteApp oClienteApp = new ClienteApp();
                oClienteApp.excluir(oCliente);
                MessageBox.Show("Cliente excluido com sucesso!",
        "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpaTela();
                montaGrid();
                txtNomeCliente.Focus();
            }
            catch (Exception oErro)
            {
                MessageBox.Show(oErro.Message, "Erro",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
