using System;

namespace Opgave_5_TCP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerWorker worker = new ServerWorker();
            worker.Start();
        }
    }
}
