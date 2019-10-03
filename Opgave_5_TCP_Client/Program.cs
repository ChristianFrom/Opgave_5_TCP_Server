using System;

namespace Opgave_5_TCP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientWorker worker = new ClientWorker();
            worker.Start();

            Console.ReadKey();
        }
    }
}
