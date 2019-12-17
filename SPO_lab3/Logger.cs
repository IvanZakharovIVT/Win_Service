using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.IO;
using System.Threading;

namespace SPO_lab3
{
    class Logger
    {
        const int port = 8888;
        bool enabled = true;
        public Logger()
        {
            
        }

        public void Start()
        {
            while (enabled)
            {
                TcpListener server = null;
                try
                {
                    IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                    server = new TcpListener(localAddr, port);

                    // запуск слушателя
                    server.Start();
                    Console.WriteLine("Ожидание подключений... ");
                    
                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");

                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();

                    // сообщение для отправки клиенту
                    string response = "Привет мир";
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(response);

                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Отправлено сообщение: {0}", response);
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (server != null)
                        server.Stop();
                }
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            enabled = false;
        }
    }
}