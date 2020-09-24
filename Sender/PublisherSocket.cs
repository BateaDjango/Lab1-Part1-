using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender
{
    class PublisherSocket
    {
        private Socket socket;
        public bool isConnected;
        public PublisherSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect(string ipaddress, int port)
        {
            socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipaddress), port), ConnectedCallBack, null);
            Thread.Sleep(2000);
            //When the asynchron connection of BeginConnect starts the ConnectedCallBack function will be called
        }
        private void ConnectedCallBack(IAsyncResult asyncResult)
        {
            if (socket.Connected)
            {
                Console.WriteLine("Sender connected to Broker.");

            }
            else MessageBox.Show("Sender Could not connect to Broker. Please try again !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            isConnected = socket.Connected;
        }

        public void Send(byte[] data)
        {
            try
            {
                socket.Send(data);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Could not send data {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
