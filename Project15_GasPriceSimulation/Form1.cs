using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project15_GasPriceSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        double dieselPrice = 0;
        double gasolinePrice = 0;
        double lpgPrice = 0;
        double gasAmount;
        double dieselAmount;
        double LpgAmount;
        double progressBarValue = 0;
        double totalPrice = 0;
        int count = 0;

        private void btnStart_Click(object sender, EventArgs e)
        {
            gasAmount = Convert.ToDouble(txtGasAmount.Text);
            dieselAmount = Convert.ToDouble(txtGasAmount.Text);
            timer1.Start();
            timer1.Interval= 100;  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            
            this.Text = count.ToString();
            if (rdbGasoline.Checked)
            {
                count++;
                if (count<=gasAmount)
                {
                    totalPrice += gasolinePrice;
                    txtTotalPrice.Text = totalPrice.ToString() + "₺";
                }
                else
                {
                    txtTotalPrice.Text = totalPrice.ToString() + "₺";
                }
               
                progressBar1.Value += 1;
                if (progressBar1.Value==99)
                {
                    timer1.Stop();
                }
            }

            if (rdbDiesel.Checked)
            {
                count++;
                if (count <= dieselAmount)
                {
                    totalPrice += dieselPrice;
                    txtTotalPrice.Text = totalPrice.ToString() + "₺";
                }
                else
                {
                    txtTotalPrice.Text = totalPrice.ToString() + "₺";
                }

                progressBar1.Value += 1;
                if (progressBar1.Value == 99)
                {
                    timer1.Stop();
                }
            }

            if (rdbLPG.Checked)
            {
                count++;
               
                if (count <= LpgAmount)
                {
                    totalPrice += lpgPrice;
                    txtTotalPrice.Text = totalPrice.ToString() + "₺";
                }
                else
                {
                    txtTotalPrice.Text = totalPrice.ToString() + "₺";
                }

                progressBar1.Value += 1;
                if (progressBar1.Value == 99)
                {
                    timer1.Stop();
                }
            }
        
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://akaryakit-fiyatlari.p.rapidapi.com/fuel-locations"),
                Headers =
    {
        { "x-rapidapi-key", "6521c51e5dmshfc9e3b3cc8c3ff5p1a49bdjsn62c66b6e0862" },
        { "x-rapidapi-host", "akaryakit-fiyatlari.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);
                var gasolinaJsonValue = json["data"][16]["prices"][0]["benzin"];
                var dieselJsonValue = json["data"][16]["prices"][0]["motorin"];
                var lpgJsonValue = json["data"][16]["prices"][0]["lpg"];
                dieselPrice=double.Parse(dieselJsonValue.ToString());
                txtGasolinePrice.Text = gasolinaJsonValue.ToString() + "₺";
                txtDiesellPrice.Text = dieselJsonValue.ToString() + "₺";
                txtLpgPrice.Text = lpgJsonValue.ToString() + "₺";
                //var fahrenheight = json["main"]["feels_like"].ToString();
            }




            //MessageBox.Show("Api Yakıt Verileri Alınıyor...");

            //txtDiesellPrice.Text = dieselPrice.ToString() + "₺";
            //txtGasolinePrice.Text = gasolinePrice.ToString() + "₺";
            //txtLpgPrice.Text = lpgPrice.ToString() + "₺";
        }
    }
}
