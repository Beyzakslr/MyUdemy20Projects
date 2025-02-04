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
using System.Xml.Linq;

namespace Project10_PostgreSQLToDoListApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localHost;port=5433;Database=DbProject10ToDoApp;user ID=postgres;Password=12345";

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCategory frm =new FrmCategory();
            frm.Show();
        }

        void ToDoList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "Select * From ToDoLists";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        void CategoryList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "Select * From Categories";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.DataSource = dataTable;
            connection.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
           ToDoList();

        }

        private void btnFullList_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "Select todolistid,title,decsription,status,priority,categoryname from" +
                "todolists inner join on categories todolists.categoryid=categories.categoryid";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToDoList();
            CategoryList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(cmbCategory.SelectedValue.ToString());
            string title = txtTitle.Text;
            string priority= txtPrivarity.Text;
            string description=txtDescription.Text;
            string status = "B'0'"; ;

            if (rdbCompleted.Checked)
            {
                status = "B'1'";
            }
            if (rdbCountinue.Checked)
            {
                status = "B'0'";
            }

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into ToDoList (title,description,status,priority,categoriId) values " +
                "(@title,@description,B'1',@priority,@categoryId)";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                //command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@categoryId", id);
                command.Parameters.AddWithValue("@priority", priority);
                command.ExecuteNonQuery();
                MessageBox.Show("Yapılacak işlem Eklendi");
                ToDoList();
            }
            connection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtid.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Delete From Categories Where ToDoList=@todolistid";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@todolistid", id);
                command.ExecuteNonQuery();
                MessageBox.Show("Veri Başarıyla Silindi");
                CategoryList();
            }
            connection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(cmbCategory.SelectedValue.ToString());
            string title = txtTitle.Text;
            string priority = txtPrivarity.Text;
            string description = txtDescription.Text;
            int categoryId=int.Parse(cmbCategory.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Update ToDoLists Set Title=@title,Description=@description,priority=@priority,categoryId=@categoryId where ToDoList=@todolistid";
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@todolistid", id);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@priority", priority);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.ExecuteNonQuery();
                MessageBox.Show("İşlem başarıyla güncellendi");
                CategoryList();
            }
            connection.Close();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "Select * From ToDoLists where Status='1'";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void btnDevam_Click(object sender, EventArgs e)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "Select * From ToDoLists where Status='0'";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
    }
}
