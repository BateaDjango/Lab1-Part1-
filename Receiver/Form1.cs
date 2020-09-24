using MyLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Receiver
{
    public partial class Form1 : Form
    {
        readonly Color clean_color = Color.Green;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            panel2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string topic;
            topic = comboBox1.Text.ToLower();
            var receiverSocket = new ReceiverSocket(topic);
            receiverSocket.Connect(Settings.Broker_Ip, Settings.Broker_Port);
            label2.Text = topic.ToUpper();
            label2.ForeColor = clean_color;
            panel1.Hide();
            panel2.Show();
            richTextBox1.ReadOnly = true;
        }

        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void Write_in_textBox(string str)
        {
            richTextBox1.Text += str;
        }
        
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
