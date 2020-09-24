using MyLibrary;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Broker
{
    public class BrockerSocket
    {
        private Socket socket;
        private const int Connections_Limit = 8;
        public BrockerSocket()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start(string ip, int port)
        {
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            socket.Listen(Connections_Limit);
            Accept();
        }
        private void Accept()
        {
            socket.BeginAccept(AcceptedCallBack, null);
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            ConnectionInfo connection = asyncResult.AsyncState as ConnectionInfo;
            try
            {
                Socket senderSocket = connection.Socket;
                SocketError response;
                int buffsize = senderSocket.EndReceive(asyncResult, out response);

                if (response == SocketError.Success)
                {
                    byte[] payload = new byte[buffsize];
                    Array.Copy(connection.Data, payload, payload.Length);


                    PayloadHandler.Handle(payload, connection);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not receive the data from the Broker !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallBack, connection);
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var address = connection.Socket.RemoteEndPoint.ToString();

                    ConnectionStorage.Remove(address);
                    connection.Socket.Close();


                }
            }

        }

        private void AcceptedCallBack(IAsyncResult asyncResult)
        {
            ConnectionInfo connection = new ConnectionInfo();

            try
            {
                connection.Socket = socket.EndAccept(asyncResult);
                connection.Address = connection.Socket.RemoteEndPoint.ToString();
                connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallBack, connection);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Can't accept. {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                Accept();
            }
        }
    }
}
