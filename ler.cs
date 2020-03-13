using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace churras_tecinfo
{
    public partial class ler : Form
    {
        MySqlConnection con = new MySqlConnection();

        MySqlCommand comando;

        MySqlDataReader leitorDados;

        string sqlpesquisa = " SELECT TB_CLIENTE.*, " +
                               " TB_LOGIN.*, " +
                               " TB_ESTADO.* " +
                               " FROM TB_CLIENTE INNER JOIN TB_ESTADO " +
                               " ON TB_CLIENTE.ID_EST = TB_ESTADO.ID_EST " +
                               " LEFT JOIN TB_LOGIN " +
                               " ON TB_CLIENTE.ID_CLI = TB_LOGIN.ID_CLI " +
                               " WHERE TB_CLIENTE.ID_CLI = ";


        public ler()
        {
            InitializeComponent();
            con.ConnectionString = "server=localhost ; database=BD_PROJ ; user id=root ; password=";

            con.Open();
        }
        public ler(int x)
        {
            InitializeComponent();
            this.textBox1.Text = x.ToString();

            con.ConnectionString = "server=localhost ; database=BD_PROJ ; user id=root ; password=";

            con.Open();

            sqlpesquisa += textBox1.Text;

            comando = new MySqlCommand(sqlpesquisa, con);

            leitorDados = comando.ExecuteReader();

            if (leitorDados.HasRows)
            {

                leitorDados.Read();

                txtnome.Text = leitorDados["nome_cli"].ToString();
                txtend.Text = leitorDados["end_cli"].ToString();
                txtnum.Text = leitorDados["n_cli"].ToString();
                txtbairro.Text = leitorDados["bairro_cli"].ToString();
                txtcidade.Text = leitorDados["cidade_cli"].ToString();

                int i = int.Parse(leitorDados["id_est"].ToString());
                //comboBox1.SelectedIndex = (i - 1);
                comboBox1.Text = leitorDados["nome_est"].ToString();

                if (leitorDados["login_log"] != String.Empty)
                {
                    txtlogin.Text = leitorDados["login_log"].ToString();
                    txtsenha.Text = leitorDados["senha_log"].ToString();
                }

                button1.Visible = false;
                textBox1.Enabled = false;

                leitorDados.Close();
            }
        }

        private void ler_Load(object sender, EventArgs e)
        {
            //con.ConnectionString = "server=localhost ; database=BD_PROJ ; user id=root ; password=";

            //con.Open();

            string MySqlEstado = "SELECT * FROM TB_ESTADO";

            comando = new MySqlCommand(MySqlEstado, con);

            leitorDados = comando.ExecuteReader();

            while(leitorDados.Read())
            {
                comboBox1.Items.Add(leitorDados["NOME_EST"]);   

            }

            leitorDados.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menu frm = new menu();
            this.Hide();
            con.Close();
            frm.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string sqlpesquisa = "SELECT * FROM TB_CLIENTE WHERE ID_CLI=" + textBox1.Text;

            sqlpesquisa += textBox1.Text;

            Console.WriteLine(sqlpesquisa);
            comando = new MySqlCommand(sqlpesquisa, con);

            leitorDados = comando.ExecuteReader();

            if (leitorDados.HasRows)
            {

                leitorDados.Read();

                txtnome.Text = leitorDados["nome_cli"].ToString();
                txtend.Text = leitorDados["end_cli"].ToString();
                txtnum.Text = leitorDados["n_cli"].ToString();
                txtbairro.Text = leitorDados["bairro_cli"].ToString();
                txtcidade.Text = leitorDados["cidade_cli"].ToString();
                comboBox1.Text = leitorDados["id_est"].ToString();

                int i = int.Parse(leitorDados["id_est"].ToString());
                comboBox1.SelectedIndex = (i - 1);

                if (leitorDados["login_log"] != String.Empty)
                {
                    txtlogin.Text = leitorDados["login_log"].ToString();
                    txtsenha.Text = leitorDados["senha_log"].ToString();
                }

            }
            else
            {
                MessageBox.Show("Usuario não identificado!", " ", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            leitorDados.Close();
        }
    }
}
