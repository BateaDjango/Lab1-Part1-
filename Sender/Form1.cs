using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLibrary;
using Newtonsoft.Json;

namespace Sender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            button1.Focus();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string API_URL = "https://api.covid19api.com/summary";
            int Sleep_Delay = 300;
            string countryinfo="";
            string str1 = ""; string str2 = ""; string str3 = ""; string str4 = ""; string str5 = ""; string str6 = "";
            PublisherSocket publisherSocket = new PublisherSocket();
            publisherSocket.Connect(Settings.Broker_Ip, Settings.Broker_Port);
            if (publisherSocket.isConnected)
            {
            using (WebClient client = new WebClient())
            {
                var json = client.DownloadString(API_URL);
                Console.WriteLine($"Sent {json}\n\n");
                CovidData obj1 = JsonConvert.DeserializeObject<CovidData>(json);
                foreach (Country cs in obj1.Countries)
                {
                    if (cs.country.Equals(comboBox1.Text))
                    {

                        if (checkBox1.Checked == true)
                            str1 = cs.NewConfirmed.ToString();
                        if (checkBox2.Checked == true)
                            str2 = cs.NewDeaths.ToString();
                        if (checkBox3.Checked == true)
                            str3 = cs.NewRecovered.ToString();
                        if (checkBox4.Checked == true)
                            str4 = cs.TotalConfirmed.ToString();
                        if (checkBox5.Checked == true)
                            str5 = cs.TotalDeaths.ToString();
                        if (checkBox6.Checked == true)
                            str6 = cs.TotalRecovered.ToString();
                            for (int i = 1; i < 7; i++)
                            {
                                if (i == 1)
                                    if (!str1.Equals("")) countryinfo += $"New Cases confirmed: {str1}\n";
                                if (i == 2)
                                    if (!str2.Equals("")) countryinfo += $"New Deaths confirmed: {str2}\n";
                                if (i == 3)
                                    if (!str3.Equals("")) countryinfo += $"New Recovered cases: {str3}\n";
                                if (i == 4)
                                    if (!str4.Equals("")) countryinfo += $"Total Cases Confirmed: {str4}\n";
                                if (i == 5)
                                    if (!str5.Equals("")) countryinfo += $"Total Deaths: {str5}\n";
                                if (i == 6)
                                    if (!str6.Equals("")) countryinfo += $"Total Recovered: {str6}\n\n";

                            }

                    }
                } 
            }


                var payload = new Payload();
                payload.Topic = comboBox1.Text.ToLower();
                payload.Message = countryinfo;
                var payloadString = JsonConvert.SerializeObject(payload);
                byte[] data = Encoding.UTF8.GetBytes(payloadString);
                publisherSocket.Send(data);
            }
            else
            {
                MessageBox.Show("Could not get the connection to the Broker !");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public bool check_enabled()
        {
            if (checkBox1.Checked == true || checkBox2.Checked == true || checkBox3.Checked == true || checkBox4.Checked == true || checkBox5.Checked == true || checkBox6.Checked == true)
                return true;
            else
            {
                return false;
            }
                
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (!check_enabled())
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (!check_enabled())
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (!check_enabled())
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (!check_enabled())
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (!check_enabled())
                button1.Enabled = false;
            else button1.Enabled = true;
        }

        private void checkBox6_CheckStateChanged(object sender, EventArgs e)
        {
            if (!check_enabled())
                button1.Enabled = false;
            else button1.Enabled = true;
        }
    }
}
