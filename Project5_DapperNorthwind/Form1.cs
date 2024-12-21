using Dapper;
using Project5_DapperNorthwind.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project5_DapperNorthwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection( "Server=(localdb)\\MSSQLLocalDB;initial catalog=Db5Project20;integrated security=true");


        private async void btnCategoryList_Click(object sender, EventArgs e)
        {
            string query = "Select * From Categories";
            var values=await connection.QueryAsync<ResultCategoryDto>(query);
            dataGridView1.DataSource = values;

        }

        private async void btnCreateCategory_Click(object sender, EventArgs e)
        {
            string sql = "insert into Categories (CategoryName, Description) Values (@p1, @p2)";
            var parameteres = new DynamicParameters();
            parameteres.Add("@p1", txtCategoryName.Text);
            parameteres.Add("@p2", txtCategoryDescription.Text);
            await connection.ExecuteAsync(sql, parameteres);
        }

        private async void btnCategoryDelete_Click(object sender, EventArgs e)
        {
            string query = "Delete from Categories Where CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryId", txtCateoryId.Text);
            await connection.ExecuteAsync(query, parameters);

        }

        private async void btnCategoryUpdate_Click(object sender, EventArgs e)
        {
            string query = "Update Categories Set CategoryName=@categoryName, Description=@description Where CategoryId=@categoryId";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", txtCategoryName.Text);
            parameters.Add("@description", txtCategoryDescription.Text);
            parameters.Add("@categoryId", txtCateoryId.Text);
            await connection.ExecuteAsync(query, parameters);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
          
            //Kategori Sayısı
            string query = "Select Count(*) From Categories";
            var CategoryCount = await connection.ExecuteScalarAsync<int>(query);
            lblCategoryCount.Text = "Toplam Kategori Sayısı: " + CategoryCount;

            //Ürün sayısı
            string query2 = "Select Count(*) From Products";
            var ProductCount = await connection.ExecuteScalarAsync<int>(query2);
            lblProductCount.Text = "Toplam ürün sayısı: " + ProductCount;

            //Ortalama Ürün Stok Satısı
            string query3 = "Select Avg(UnitsInStock) From products";
            var avgProductCount = await connection.ExecuteScalarAsync<int>(query3);
            lblAvgProductStock.Text = "Ortalama Ürün sayısı: " + avgProductCount;

            //Deniz ürünleri fiyatı
            string query4 = "Select Sum(UnitPrice) From Products Where CategoryId=(Select CategoryId From Categories Where CategoryName='SeaFood')";
            var totalSeaFoodPrice = await connection.ExecuteScalarAsync<decimal>(query4);
            lblSeafoodProductTotalPrice.Text = "Deniz ürünleri toplam fiyatı: " + totalSeaFoodPrice;
        }
    }
}
