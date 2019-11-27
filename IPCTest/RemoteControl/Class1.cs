
using System;
using System.Linq;
using System.Collections.Generic;

namespace RemoteControl
{
    public class RemoteObject : MarshalByRefObject
    {
        static Queue<string> ClientMsg = new Queue<string>();
        static Queue<string> ServerMsg = new Queue<string>();

        public void ServerToClient(string msg)
        {
            ClientMsg.Enqueue(msg);
        }

        public void ClientToServer(string msg)
        {
            ServerMsg.Enqueue(msg);
        }

        public string GetClientMessage()
        {
            if ( ClientMsg.Count() < 1)
            {
                return "";
            }

            var msg = ClientMsg.Dequeue();
            return msg;
        }

        public string GetServerMessage()
        {
            if ( ServerMsg.Count() < 1)
            {
                return "";
            }

            var msg = ServerMsg.Dequeue();
            return msg;
        }
    }
}
