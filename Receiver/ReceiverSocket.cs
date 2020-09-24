using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using MyLibrary;
namespace Receiver
{
    class ReceiverSocket
    {
        private Socket socket;
        private string topic;

        public ReceiverSocket(string topic1)
        {
            topic = topic1;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect(string ipAddress, int port)
        {
            socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnectedCallback, null);
            
        }
        private void ConnectedCallback(IAsyncResult asyncResult)
        {
            if (socket.Connected)
            {
                Subscribe();
                StartReceive();
            }
            else
            {
                MessageBox.Show("User could not connect to Broker", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Subscribe()
        {

            var data = Encoding.UTF8.GetBytes("#" + topic);
            Send(data);

        }
        private void StartReceive()
        {
            ConnectionInfo connection = new ConnectionInfo();
            connection.Socket = socket;
            socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallback, connection); ;
        }
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            ConnectionInfo connectionInfo = asyncResult.AsyncState as ConnectionInfo;
            try
            {
                SocketError response;
                int buffSize = socket.EndReceive(asyncResult, out response);

                if (response == SocketError.Success)
                {
                    byte[] payloadBytes = new byte[buffSize];
                    Array.Copy(connectionInfo.Data, payloadBytes, payloadBytes.Length);



                    PayloadHandler.Handle(payloadBytes);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Could not receive the data from the Broker ! {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                try
                {
                    connectionInfo.Socket.BeginReceive(connectionInfo.Data, 0, connectionInfo.Data.Length, SocketFlags.None, ReceiveCallback, connectionInfo);

                }
                catch (Exception e)
                {
                    //MessageBox.Show($"Error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connectionInfo.Socket.Close();
                }
            }
        }

        private void Send(byte[] data)
        {
            try
            {
                socket.Send(data);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Could not send the data {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
