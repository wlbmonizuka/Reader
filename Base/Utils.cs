using System;
using System.Text;
using System.Threading;

namespace Base
{
    public class Utils
    {
        //Запуск нового потока с обработкой ошибок
        public static void StartThread(Action onException, Action act) =>
            new Thread(() =>
            {
                try
                {
                    act();
                }
                catch //(Exception ex)
                {
                    //Если захочется подебажить
                    //Console.WriteLine(ex);
                    onException();
                }
            }).
            Start();

        //Вывод массива байтов в виде строки
        public static string ToHexStr(byte[] bytes, string strSeparator = " ")
        {
            if (bytes.Length == 0)
                return String.Empty;

            var sb = new StringBuilder((2 + strSeparator.Length) * bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                sb.AppendFormat("{0:X2}{1}", b, strSeparator);
            }

            return sb.ToString(0, sb.Length - strSeparator.Length);
        }

        static object lockObj = new object();
        public static void Log(ConsoleColor color, string msg)
        {
            lock (lockObj)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(msg);
            }
        }
    }
}