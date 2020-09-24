using MyLibrary;
using System.Collections.Concurrent;

namespace Broker
{
    static class PayloadStorage
    {
        private static ConcurrentQueue<Payload> payloadQueue;
        static PayloadStorage()
        {
            payloadQueue = new ConcurrentQueue<Payload>();
        }

        public static void Add(Payload payload)
        {
            payloadQueue.Enqueue(payload);
        }

        public static Payload GetNext()
        {
            Payload payload = null;
            payloadQueue.TryDequeue(out payload);
            return payload;
        }
        public static bool IsEmpty()
        {
            return payloadQueue.IsEmpty;
        }

    }
}
