using System;
using System.IO;
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

        public static void StartNew(Socket socket, bool resend = false) =>
            new SocketWorker(socket).Start(resend);

        public void Start(bool resend = false)
        {
            Utils.Log(ConsoleColor.DarkGreen, $"Connected {((IPEndPoint)socket.RemoteEndPoint).Port}");
            var stream = new NetworkStream(socket);
            var br = new BinaryReader(stream);
            var bw = new BinaryWriter(stream);

            var reader = new PacketReader(br);
            var writer = new PacketWriter(bw);
            if (resend)
                reader.OnRead += (q, qq) =>
                {
                    bw.WriteTag(q);
                    bw.WritePacket(qq);
                };

            //Запускаем в новом потоке чтение из подключенного клиента, чтобы не задерживать вызывающий поток
            Utils.StartThread(OnDisconnect, reader.Start);

            //Запускаем в новом потоке инжект в подключенного клиента, чтобы не задерживать вызывающий поток
            Utils.StartThread(OnDisconnect, writer.Start);

            void OnDisconnect() =>
                Utils.Log(ConsoleColor.DarkRed, $"Disconnected {((IPEndPoint)socket.RemoteEndPoint).Port}");
        }
    }
}