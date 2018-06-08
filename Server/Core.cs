using Base;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Core
    {
        public void Start()
        {
            //Слушаем порт
            var listener = new TcpListener(IPAddress.Any, SocketWorker.Port);
            listener.Start();

            //Принимаем подключения клиентов
            while (true)
            {
                var socket = listener.AcceptSocket();
                SocketWorker.StartNew(socket, true);
            }
        }
    }
}