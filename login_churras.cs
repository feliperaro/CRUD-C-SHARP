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
using churras_tecinfo.modelo;


namespace churras_tecinfo
{
    public partial class login_churras : Form
    {
        //conexão com o banco atráves de obj
        MySqlConnection con = new MySqlConnection();

        // objeto para transformar string em comandos sql
        MySqlCommand comando;

        //objeto para capturar os valores lidos no banco
        MySqlDataReader tabelaDados;
        
        //fazer uma string para query ao banco
        string sql_login; 

        public login_churras()
        {
            InitializeComponent();
       
        }

        private void login_churras_Load(object sender, EventArgs e)
        {
            //fazer um caminho fisico do servidor
            con.ConnectionString = "server=localhost ; database=BD_PROJ ; user id=root ; password=";

            con.Open();

            //como saber se o banco está conectado
            //MessageBox.Show(con.State.ToString());

           // comando = new MySqlCommand(sql_login, con);

            //comando para executar a query no sql
            //* ExecuteNonQuery - comando que e usado no(insert, delete e update) não retorna nenhum valor

            //* ExecuteReader - usado no comando select(retorna os dados)

            //* ExecuteScalar() - retorna apenas um valor

            //executar o comando 
            //tabelaDados = comando.ExecuteReader();

            //perguntar se houve linhas retornadas(bool)
          //  MessageBox.Show(tabelaDados.HasRows.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Campo login obrigatório", "TEC 8", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                textBox1.Focus();

                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Campo senha obrigatório", "TEC 8", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                textBox2.Focus();

                return;
            }

            if (!String.IsNullOrWhiteSpace(textBox1.Text) &&
                (!String.IsNullOrWhiteSpace(textBox2.Text)))
            {

                sql_login = "select * from tb_login where login_log = '" + textBox1.Text + "'  and senha_log = '" + textBox2.Text + "';";


                comando = new MySqlCommand(sql_login, con);
                tabelaDados = comando.ExecuteReader();
                //MessageBox.Show(tabelaDados.HasRows.ToString());

                if (tabelaDados.HasRows == true)
                {
                    tabelaDados.Read();
                    int idCliente = int.Parse(tabelaDados["id_cli"].ToString());

                    IdentificaUsuario us = new IdentificaUsuario(idCliente);


                    MessageBox.Show("Seja Bem-Vindo! " + textBox1.Text, "Olá", MessageBoxButtons.OK);
                    menu frm = new menu();
                    this.Hide();
                    frm.ShowDialog();
                }
                if (tabelaDados.HasRows == false)
                {
                    MessageBox.Show("Login ou senha incorretos", "Tente novamente", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

                tabelaDados.Close();
            }

            con.Close();

            Console.WriteLine(sql_login);


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Consulte um administrador e peça para que seja feito a busca da sua senha", "",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
    }
}
