using Newtonsoft.Json;
using Obligatorisk_Opgave_1_Unit_Test;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Opgave_5_TCP_Server
{
    public class ServerWorker
    {

        private static List<Book> books = new List<Book>()
        {
            new Book("Shrek 5", "Peter Pedal", 333, "1111111111111"),
            new Book("Fed bog", "Tolkien", 666, "2222222222222"),
            new Book("Shrek 10", "Peter Pedal", 1000, "3333333333333"),
        };


        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 4646);
            server.Start();
            Console.WriteLine("Server");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient(); // Venter på client

                // Starter en ny tåd
                Task.Run(
                    // Indsætter en metode (delegate)
                    () =>
                    {
                        TcpClient tmpsocket = client;
                        GetBook(tmpsocket);
                    }
                );
            }
        }




        public void GetBook(TcpClient tmpsocket)
        {
            using (StreamReader sr = new StreamReader(tmpsocket.GetStream()))
            using (StreamWriter sw = new StreamWriter(tmpsocket.GetStream()))
            {
                while (true)
                {
                    
                    string str = sr.ReadLine().ToLower();
                    string str2 = sr.ReadLine().ToLower();
                    if (str == "hent alle")
                    {
                        foreach (Book book in books)
                        {
                            string jsonBook = JsonConvert.SerializeObject(book);
                            Console.WriteLine(jsonBook);
                            sw.WriteLine(jsonBook);
                        }
                    }
                    else if (str == "hent" )
                    {
                        string jsonStr = JsonConvert.SerializeObject(books.Find(i => i.Isbn13 == str2));
                        sw.WriteLine(jsonStr);
                    }
                    else if (str == "gem")
                    {
                        string stringToRead = sr.ReadLine();
                        Book book = JsonConvert.DeserializeObject<Book>(stringToRead);
                        books.Add(book);
                    }

                    sw.Flush();
                }
            }
        }



       
    }
}
