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
                    string[] strings = str.Split(" ");
                    if (strings[0] == "hentalle")
                    {
                        sw.WriteLine("Test");
                        foreach (Book book in books)
                        {
                            string jsonBook = JsonConvert.SerializeObject(book);
                            Console.WriteLine(jsonBook);
                            sw.WriteLine(jsonBook);
                        }
                    }
                    else if (strings[0] == "hent" && strings[1].Length == 13)
                    {
                        Book jsonBook = bookSortList(strings[1]);
                        string jsonSend = JsonConvert.SerializeObject(jsonBook);
                        Console.WriteLine(jsonSend);
                        sw.WriteLine(jsonSend);
                    }
                    else if (strings[0] == "gem")
                    {
                        string stringToRead = sr.ReadLine();
                        Book book = JsonConvert.DeserializeObject<Book>(stringToRead);
                        books.Add(book);
                    }

                    sw.Flush();
                }
            }
        }



        public Book bookSortList(string myarray)
        {
            Book myBook = new Book();
            foreach (var book in books)
            {
                if (book.Isbn13 == myarray)
                {
                    myBook = book;
                }
            }

            return myBook;
        }
    }
}
