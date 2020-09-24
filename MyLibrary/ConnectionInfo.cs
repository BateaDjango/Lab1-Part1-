using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MyLibrary
{
    public class ConnectionInfo
    {
        public const int Buffer_Size = 4096;
        public byte[] Data { get; set; }
        public Socket Socket { get; set; }
        public string Address { get; set; }

        public string Topic { get; set; }

        public ConnectionInfo()
        {
            Data = new byte[Buffer_Size];
        }
    }
}
