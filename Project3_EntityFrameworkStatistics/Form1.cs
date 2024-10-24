using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_EntityFrameworkStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Db3Project20Entities db = new Db3Project20Entities();

        private void Form1_Load(object sender, EventArgs e)
        {
            //Toplam kategori sayısı
            int categoryCount = db.TblCategory.Count();
            lblCategoryCount.Text=categoryCount.ToString();

            //Toplam Ürün Sayısı
            int productCount = db.TblProduct.Count();
            lblProductCount.Text=productCount.ToString();

            //Toplammüşteri sayısı
            int customerCount = db.TblCustomer.Count();
            lblCustomerCount.Text=customerCount.ToString();

            //Toplam sipariş sayısı
            int orderCount = db.TblOrder.Count();
            lblOrderCount.Text=orderCount.ToString();

            //Toplam stok sayısı
            var totalProductStockCount = db.TblProduct.Sum(x => x.ProductStock);
            lblProductTotalStock.Text=totalProductStockCount.ToString();
        }
    }
}
