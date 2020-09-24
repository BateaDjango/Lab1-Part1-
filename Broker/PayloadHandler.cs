using MyLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Broker
{
    class PayloadHandler
    {
        public static void Handle(byte[] payloadBytes, ConnectionInfo connectionInfo)
        {
            var payloadString = Encoding.UTF8.GetString(payloadBytes);

            if (payloadString.StartsWith("#"))
            {
                connectionInfo.Topic = payloadString.Split('#').LastOrDefault();

                ConnectionStorage.Add(connectionInfo);
            }
            else
            {
                Payload payload = JsonConvert.DeserializeObject<Payload>(payloadString);

                PayloadStorage.Add(payload);
                Console.WriteLine(payloadString);
            }


        }
    }
}
