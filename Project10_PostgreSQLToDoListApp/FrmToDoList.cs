using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project10_PostgreSQLToDoListApp
{
    public partial class FrmToDoList : Form
    {
        public FrmToDoList()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localHost;port=5433;Database=DbProject10ToDoApp;user ID=postgres;Password=12345";

        private void FrmToDoList_Load(object sender, EventArgs e)
        {
            // NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;port=5433;Database=DbProject10ToDoApp;user ID=postgres;Password=12345");

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "Select * From todolists";
            var command = new NpgsqlCommand(query, connection);
            var adapter=new NpgsqlDataAdapter(command);
            DataTable dataSet = new DataTable();
            adapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet;
            connection.Close();

        }
    }
}
