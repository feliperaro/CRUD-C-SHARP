using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using churras_tecinfo.modelo;
using MySql.Data.MySqlClient;


namespace churras_tecinfo
{
    public partial class menu : Form
    {
        Timer tm = new Timer();
        public menu()
        {
            InitializeComponent();
            this.tm.Enabled = true;
            this.tm.Interval = 1000;
            tm.Tick += Tm_Tick;
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            hora.Text = DateTime.Now.ToString();
        }

        private void menu_Load(object sender, EventArgs e)
        {
            hora.Text = DateTime.Now.ToString();

            toolStripLabel1.Text = IdentificaUsuario.nomeUsuario;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process1.StartInfo.FileName = ("Calc.exe");

            process1.Start();
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process1.StartInfo.FileName = (@"C:\Program Files\Microsoft Office\Office16\WINWORD.EXE");

            process1.Start();
        }

        private void chromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            process1.StartInfo.FileName = (@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");

            process1.Start();
        }

        private void criarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastro_Criar cadastro = new cadastro_Criar();
            this.Hide();
            cadastro.ShowDialog();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            //toolStrip1.Text = 
        }

        private void lerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ler consulta = new ler();
            this.Hide();
            consulta.ShowDialog();
        }

        private void atualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            atualizar att = new atualizar();
            this.Hide();
            att.ShowDialog();
        }

        private void deletarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deletar del = new deletar();
            this.Hide();
            del.ShowDialog();
        }

        private void listaDeUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaClienteBanco list = new ListaClienteBanco();
            this.Hide();
            list.ShowDialog();
        }

        private void pesquisarUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PesquisaNome pesq = new PesquisaNome();
            this.Hide();
            pesq.ShowDialog();
        }
    }
}
