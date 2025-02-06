using Project12_JwtToken.JWT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project12_JwtToken
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        public string token;
        SqlConnection sqlConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;initial " +
             "catalog=Db12Project20;integrated security=true");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            TokenGenerator tokenGenerator = new TokenGenerator();

            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From TblUser Where Username=@usurname and Password=@password",sqlConnection);
            command.Parameters.AddWithValue("@username", txtName.Text);
            command.Parameters.AddWithValue("@password", txtPassword.Text);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.Read())
            {
                string token=tokenGenerator.GeneratorJwtToken2(txtName.Text);
                FrmEmployee frm = new FrmEmployee();
                frm.tokenGet = token;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı ya da şifre");
                txtPassword.Clear();
                txtName.Clear();
                txtName.Focus();
            }
            sqlConnection.Close();
        }
    }
}
