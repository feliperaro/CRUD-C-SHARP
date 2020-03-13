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
    public partial class ListaClienteBanco : Form
    {
        MySqlConnection con;

        MySqlDataAdapter adapter;

        public ListaClienteBanco()
        {
            InitializeComponent();
        }

        private void ListaClienteBanco_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            con = new MySqlConnection("server=localhost ; database=BD_PROJ ; user id=root ; password=");

            string sqlista = " SELECT TB_CLIENTE.ID_CLI AS CODIGO, " +
                             " TB_CLIENTE.NOME_CLI AS NOME, " +
                             " TB_CLIENTE.END_CLI AS ENDERECO, " +
                             " TB_CLIENTE.N_CLI AS NUMERO, " +
                             " TB_CLIENTE.BAIRRO_CLI AS BAIRRO, " +
                             " TB_CLIENTE.CIDADE_CLI AS CIDADE, " +
                             " TB_ESTADO.NOME_EST AS ESTADO " +
                             " FROM TB_CLIENTE INNER JOIN TB_ESTADO " +
                             " ON TB_CLIENTE.ID_EST = TB_ESTADO.ID_EST ";

            adapter = new MySqlDataAdapter(sqlista, con);

            DataSet conjuntoDados = new DataSet();

            adapter.Fill(conjuntoDados);

            dataGridView1.DataSource = conjuntoDados.Tables[0];

                            
        }

        private void ListaClienteBanco_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu frm = new menu();
            this.Hide();
            con.Close();
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
    }
}
