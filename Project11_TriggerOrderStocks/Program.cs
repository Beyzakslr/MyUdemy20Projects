using Project11_TriggerOrderStocks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Project11_TriggerOrderStocks
{
    internal class Program
    {
         

        static void Main(string[] args)
        {
            Db11Project20Entities context = new Db11Project20Entities();

            string number;
            Console.WriteLine("### Sipariş Stok Sistemi");
            Console.WriteLine();
            Console.WriteLine("1-Ürün Listesi");
            Console.WriteLine("2-Sipariş Listesi");
            Console.WriteLine("3-Kasa Durumu");
            Console.WriteLine("4-Yeni Ürün Satışı");
            Console.WriteLine("5-İşlem Sayacı");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine();

            Console.Write("Lütfen yapmak istediğiniz işlemi seçiniz: ");
            number = Console.ReadLine();
            Console.WriteLine( );

            if (number == "1")
            {
                Console.WriteLine("----Ürün Listesi----");

                var values = context.TblOrder.ToList();
                foreach(var item in values)
                {
                    Console.WriteLine(item.OrderId + "-" + item.TblProduct.ProductName + "Birim Fiyat: " +  item.UnitPrice + 
                        "Adet: " + item.Quantity + "Toplam Fiyat: " + item.TotalPrice);
                }

                
            }
            if (number=="2")
            {
                Console.WriteLine("----Sipariş Listesi----");

                var values = context.TblProduct.ToList();
                foreach (var item in values)
                {
                    Console.WriteLine(item.ProductId + "-" + item.ProductName + "Stok Sayısı" + item.ProductStock +
                        "Fiyatı: " + item.ProductPrice + "TL");
                }
            }
            if(number=="3")
            {
                Console.WriteLine("----Kasa Durumu----");

                var values = context.TblCashRegister.Select(x=>x.CashRegisterBalance).FirstOrDefault();
                Console.Write("Kasadaki Toplam tutar: " + values + "TL");
            }
            if (number=="4")
            {
                Console.WriteLine("----Yeni Ürün Satışı----");

                Console.Write("Müşteri Adı: ");
                string customer = Console.ReadLine();

                Console.Write("Ürün Id: ");
                int productId =int.Parse(Console.ReadLine());

                Console.Write("Ürün Adedi: ");
                int quantity = int.Parse(Console.ReadLine());

                
                

                var productName=context.TblProduct.Where(x=>x.ProductId== productId).Select(y=>y.ProductName).FirstOrDefault();

                var productUnitPrice= context.TblProduct.Where(x=>x.ProductId == productId).Select(y=>y.ProductPrice).FirstOrDefault();

                Console.WriteLine("Ürün Adı:" + productName);

                Console.WriteLine("Bitrim Fiyat: " + productUnitPrice);

                decimal totalPrice = quantity + decimal.Parse(productUnitPrice.ToString());
                Console.Write("Toplam Fiyat: " + totalPrice );


                Console.WriteLine();

                Console.WriteLine("---- Ürün Bilgileri -----");

                TblOrder tblOrder = new TblOrder();
                tblOrder.UnitPrice = productUnitPrice;
                tblOrder.ProductId = productId;
                tblOrder.Quantity = quantity;
                tblOrder.TotalPrice = totalPrice;
                tblOrder.Customer = customer;


                context.TblOrder.Add(tblOrder);
                context.SaveChanges();




            }
            if (number=="5")
            {
                var value = context.TblProcess.Select(x => x.Process).FirstOrDefault();
                Console.WriteLine("Toplam işlem sayısı: " + value);
            }

            Console.Read();
        }
    }
}
