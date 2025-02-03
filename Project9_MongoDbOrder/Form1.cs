using Project9_MongoDbOrder.Entities;
using Project9_MongoDbOrder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project9_MongoDbOrder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OrderOperation orderOperation = new OrderOperation();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var order = new Order
            {
                City = txtCity.Text,
                CustomerName = txtCustomerName.Text,
                District = txtDistrict.Text,
                TotalPrice = decimal.Parse(txtTotalPrice.Text),
                OrderId = txtId.Text,
            };

            orderOperation.AddOrder(order);
            MessageBox.Show("Ekleme İşlmei yapıldı");

        }

        private void btnList_Click(object sender, EventArgs e)
        {
            List<Order> orders= orderOperation.GetAllOrders();
            dataGridView1.DataSource = orders;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string orderId=txtId.Text;
            orderOperation.DeleteOrder(orderId);
            MessageBox.Show("Silme işlemi tamamlandı");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id=txtId.Text;
            var updateOrder = new Order
            {
                City = txtCity.Text,
                CustomerName = txtCustomerName.Text,
                District = txtDistrict.Text,
                OrderId = id,
                TotalPrice=decimal.Parse(txtTotalPrice.Text),
            };

            orderOperation.UpdateOrder(updateOrder);

        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            string id= txtId.Text;
            Order orders = orderOperation.GetOrderById(id);
            dataGridView1.DataSource = new List<Order> { orders };
        }
    }
}
