using System;
using System.Threading.Tasks;
using MyLibrary;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Broker");

            BrockerSocket socket = new BrockerSocket();
            socket.Start(Settings.Broker_Ip, Settings.Broker_Port);

            var worker = new Worker();
            Task.Factory.StartNew(worker.DoSendMessageWork, TaskCreationOptions.LongRunning);
            Console.ReadLine();

        }
    }
}
