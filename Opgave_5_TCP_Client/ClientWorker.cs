using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Opgave_5_TCP_Client
{
    class ClientWorker
    {
        public ClientWorker()
        {
            
        }
        public void Start()
        {
            using (TcpClient socket = new TcpClient("localhost", 4646))
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                Console.WriteLine("Client");
                string str = Console.ReadLine();

                sw.WriteLine(str);
                sw.Flush();

                string strin = sr.ReadLine();
                Console.WriteLine(strin);
            }

        }
    }
}
