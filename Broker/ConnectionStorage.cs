using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLibrary;
namespace Broker
{
    static class ConnectionStorage
    {
        private static List<ConnectionInfo> connections;
        private static object locker;

        static ConnectionStorage()
        {
            connections = new List<ConnectionInfo>();
            locker = new object();
        }

        public static void Add(ConnectionInfo connection)
        {
            lock (locker)
            {
                connections.Add(connection);
            }
        }
        public static void Remove(string address)
        {
            lock (locker)
            {
                connections.RemoveAll(x => x.Address == address);
            }
        }

        public static List<ConnectionInfo> GetConnectionsByTopic(string topic)
        {
            List<ConnectionInfo> selectedConnections;
            lock (locker)
            {
                selectedConnections = connections.Where(x => x.Topic == topic).ToList();
            }
            return selectedConnections;
        }
    }
}
