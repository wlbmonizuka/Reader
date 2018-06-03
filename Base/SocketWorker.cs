using System;
using System.Net;
using System.Net.Sockets;

namespace Base
{
    public class SocketWorker
    {
        public const int Port = 59000;

        Socket socket;
        public SocketWorker(Socket socket) =>
            this.socket = socket;

        public static void StartNew(Socket socket) =>
            new SocketWorker(socket).Start();

        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Connected {((IPEndPoint)socket.RemoteEndPoint).Port}");

            //Запускаем в новом потоке чтение из подключенного клиента, чтобы не задерживать вызывающий поток
            Utils.StartThread(OnDisconnect, () => PacketReader.StartNew(socket));

            //Запускаем в новом потоке инжект в подключенного клиента, чтобы не задерживать вызывающий поток
            Utils.StartThread(OnDisconnect, () => PacketWriter.StartNew(socket));
        }

        void OnDisconnect()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Disconnected {((IPEndPoint)socket.RemoteEndPoint).Port}");
        }
    }
}