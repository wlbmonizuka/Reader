using Base;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Core
    {
        public void Start()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Loopback, SocketWorker.Port);
            SocketWorker.StartNew(socket);
        }
    }
}