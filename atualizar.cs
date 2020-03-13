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
    public partial class atualizar : Form
    {
        MySqlConnection con = new MySqlConnection();
        
        MySqlCommand comando;

        MySqlDataReader leitorDados;

        int idEstado;


        public atualizar()
        {
            InitializeComponent();
        }

        private void atualizar_Load(object sender, EventArgs e)
        {
            con.ConnectionString = "server=localhost ; database=BD_PROJ ; user id=root ; password=";

            con.Open();

            string MySqlEstado = "SELECT * FROM TB_ESTADO";

            comando = new MySqlCommand(MySqlEstado, con);

            leitorDados = comando.ExecuteReader();

            while (leitorDados.Read())
            {
                comboBox1.Items.Add(leitorDados["NOME_EST"]);
            }

            leitorDados.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            menu frm = new menu();
            this.Hide();
            con.Close();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtnome.Enabled = txtend.Enabled = txtnum.Enabled = txtcidade.Enabled
                = txtbairro.Enabled = comboBox1.Enabled = true;

            string sqlpesquisa = " SELECT TB_CLIENTE.*, " +
                                " TB_LOGIN.*, " +
                                " TB_ESTADO.* " +
                                " FROM TB_CLIENTE INNER JOIN TB_ESTADO " +
                                " ON TB_CLIENTE.ID_EST = TB_ESTADO.ID_EST " +
                                " LEFT JOIN TB_LOGIN " +
                                " ON TB_CLIENTE.ID_CLI = TB_LOGIN.ID_CLI " +
                                " WHERE TB_CLIENTE.ID_CLI = " + textBox1.Text;


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
                

                int i = int.Parse(leitorDados["id_est"].ToString());
                comboBox1.SelectedIndex = (i - 1);

                //checkBox1.Checked = false;

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

        private void button2_Click(object sender, EventArgs e)
        {
            

            idEstado += 1;

            string sqlUpdate = " Update tb_cliente " +
                               " set nome_cli = '" + txtnome.Text + "' , " +
                               " end_cli = '" + txtend.Text + "' , " +
                               " n_cli = '" + txtnum.Text + "' , " +
                               " cidade_cli = '" + txtcidade.Text + "' , " +
                               " bairro_cli = '" + txtbairro.Text + "' , " +
                               " id_est = " + idEstado +
                               " where id_cli = " + textBox1.Text;

            comando = new MySqlCommand(sqlUpdate, con);

            int afetadoLinhas = comando.ExecuteNonQuery();

           

            if (afetadoLinhas > 0)
            {
                if (checkBox1.Checked == true)
                {
                    try
                    {

                        string sqlAlteraLogin = " INSERT INTO TB_LOGIN (LOGIN_LOG, SENHA_LOG, ID_CLI) ";
                        sqlAlteraLogin += " VALUES ('" + txtlogin.Text + "' , '" + txtsenha.Text + "' , " + textBox1.Text + ") ";

                        comando = new MySqlCommand(sqlAlteraLogin, con);

                        comando.ExecuteNonQuery();
                    }
                    catch (Exception x)
                    {
                    }
                }

                if (txtlogin.Text != String.Empty)
                {
                   
                    string sqlLogin = " Update tb_login " +
                                      " set login_log = '" + txtlogin.Text + "', " +
                                      " senha_log = '" + txtsenha.Text + "' " +
                                      " where id_cli = " + textBox1.Text;

                    comando = new MySqlCommand(sqlLogin, con);

                    comando.ExecuteNonQuery();

                    //checkBox1.Checked = true;
                }


                MessageBox.Show("Dados alterados com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            idEstado = comboBox1.SelectedIndex;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtlogin.Enabled = txtsenha.Enabled = true;
            }
            if (checkBox1.Checked == false)
            {
                txtlogin.Enabled = txtsenha.Enabled = false;
            }
        }

    }
}
