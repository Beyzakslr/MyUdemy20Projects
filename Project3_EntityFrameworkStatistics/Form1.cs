using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

            //Ortalama Ürün Fiyatı
            var averageProductPrice=db.TblProduct.Average(x => x.ProductPrice);
            lblProductAveragePrice.Text=averageProductPrice.ToString();

            //Toplam meyve stoğu sayısı
            var totalProductCountByCategoryIsFurit = db.TblProduct.Where(x => x.CategoryId == 1).Sum(y => y.ProductStock);
            lblProductCountByCategoryIsFruit.Text=totalProductCountByCategoryIsFurit.ToString();

            //Gazoz isimli ürünün toplam işlm hacmi
            var totalPriceByProductNameIsGazozGetStock = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductStock).FirstOrDefault();
            var totalPriceByProductNameIsGazozGetUnitPrice = db.TblProduct.Where(x => x.ProductName == "Gazoz").Select(y => y.ProductPrice).FirstOrDefault();
            var totalPriceByProductNameIsGazoz = totalPriceByProductNameIsGazozGetStock * totalPriceByProductNameIsGazozGetUnitPrice;
            lblTotalPriceByProductNameIsGazoz.Text= totalPriceByProductNameIsGazoz.ToString() + " TL";

            //Stok sayısı 100'den az olan ürün sayısı
            var productCountByStockCountSmallerThen100 = db.TblProduct.Where(x => x.ProductStock < 100).Count();
            lblProductStockSmallerThen100.Text=productCountByStockCountSmallerThen100.ToString();

            //Kategorisi sebze ve durumu aktif(true) olan ürün stok toplamı
            int id = db.TblCategory.Where(x => x.CategoryName == "Sebze").Select(y => y.CategoryId).FirstOrDefault();

            var productStoclCountByCategoryNameIsSebzeAndStatusIsTrue = db.TblProduct.Where(w => w.CategoryId == 
            (db.TblCategory.Where(x => x.CategoryName == "Sebze").Select(y => y.CategoryId).FirstOrDefault()) 
            && w.ProductStatus == true).Sum(y => y.ProductStock);
            lblProductCountByCategoruSebzeAndStatusTrue.Text=productStoclCountByCategoryNameIsSebzeAndStatusIsTrue.ToString();


            //Türkiyeden yapılan siparişler SQL
            
            var orderCountFromTurkiye = db.Database.SqlQuery<int>("Select count(*) From TblOrder Where CustomerId In (Select CustomerId From TblCustomer Where\r\nCustomerCountry='Türkiye')").FirstOrDefault();
            lblOrderCountFromTurkiyeBySQL.Text= orderCountFromTurkiye.ToString();


            //Türkiyeden yapılan siparişler EF metodu
            var turkishCustomerIds = db.TblCustomer.Where(x => x.CustomerCountry == "Türkiye").Select(y=>y.CustomerId).ToList();
            var orderCountFromTurkiyeEF = db.TblOrder.Count(z => turkishCustomerIds.Contains(z.CustomerId.Value));
            lblOrderCountFromTurkiyeByEF.Text= orderCountFromTurkiyeEF.ToString();


            //Siparişler içinde kategorisi meyve olan ürünlerin toplam satış fiyatı(SQL)
           
            var orderTotalPriceByCategyIsMeyve = db.Database.SqlQuery<decimal>("Select Sum(o.TotalPrice) From TblOrder o Join TblProduct p On o.ProductId=p.ProductId Join TblCategory c On p.CategoryId=c.CategoryId where c.CategoryName='Meyve'").FirstOrDefault();
            lblOrderTotalPriceByCategyIsMeyve.Text = orderTotalPriceByCategyIsMeyve.ToString() + "TL";



            //Siparişler içinde kategorisi meyve olan ürünlerin toplam satış fiyatı(EF)
            var orderTotalPriceByCategyIsMeyveByEF = (from o in db.TblOrder
                                                      join p in db.TblProduct on o.ProductId equals p.ProductId
                                                      join c in db.TblCategory on p.CategoryId equals c.CategoryId
                                                      where c.CategoryName == "Meyve"
                                                      select o.TotalPrice).Sum();
            lblOrderTotalPriceByCategyIsMeyveByEF.Text = orderTotalPriceByCategyIsMeyveByEF.ToString() + "TL";



            //Son eklenen ürünün ismi
            var lastProductName=db.TblProduct.OrderByDescending(x=>x.ProductId).Select(y=>y.ProductName).FirstOrDefault();
            lblLastProductName.Text= lastProductName.ToString();

            //Son eklenen ürünün kategori ismi
            var lastProductCategoryId = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.CategoryId).FirstOrDefault();
            var lastProductCategoryName=db.TblCategory.Where(x=>x.CategoryId == lastProductCategoryId).Select(y=>y.CategoryName).FirstOrDefault();
            lblLastProductCategoryName.Text = lastProductCategoryName.ToString();


            //Aktif ürün sayısı
            var activeProductCount=db.TblProduct.Where(x=>x.ProductStatus ==true).Count();
            lblActiveProductCount.Text = activeProductCount.ToString();


            //Toplam kola stok satışlarından kazanılan para
            var kolaStock = db.TblProduct.Where(x => x.ProductName == "Kola").Select(y => y.ProductStock).FirstOrDefault();
            var kolaPrice=db.TblProduct.Where(x=>x.ProductName=="Kola").Select(y=>y.ProductPrice).FirstOrDefault();
            var totalPriceWithStockByKola= kolaPrice * kolaStock;
            lblTotalPriceWithStockByKola.Text= totalPriceWithStockByKola.ToString() + "TL";


            //Son sipariş veren müşterinin adı-soyadı
            var lastCustomerId = db.TblOrder.OrderByDescending(x => x.OrderId).Select(y => y.CustomerId).FirstOrDefault();
            var lastCustomerName = db.TblCustomer.Where(x => x.CustomerId == lastCustomerId).Select(y => y.CustomerName).FirstOrDefault();
            lblLastCustomerName.Text = lastCustomerName.ToString();


            //Ülke çeşitliliği sayısı
            var countryDifferentCount = db.TblCustomer.Select(x => x.CustomerCountry).Distinct().Count();
            lblCountryDifferendCount.Text= countryDifferentCount.ToString();
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
    } 
}
