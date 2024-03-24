using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

namespace QuoteGeneratorTcpServer
{
    internal class Program
    {
        private static readonly string[] Quotes =
        {
        "The only way to do great work is to love what you do. – Steve Jobs",
        "In three words I can sum up everything I've learned about life: it goes on. – Robert Frost",
        "Life is what happens when you're busy making other plans. – Allen Saunders"
    };

        static void Main()
        {
            TcpListener server = new TcpListener(IPAddress.Any, 12345);
            server.Start();
            Console.WriteLine("Server started. Waiting for clients...");

            while (true)
            {
                using (TcpClient client = server.AcceptTcpClient())
                using (NetworkStream stream = client.GetStream())
                {
                    Random random = new Random();
                    string randomQuote = Quotes[random.Next(Quotes.Length)];

                    byte[] data = Encoding.UTF8.GetBytes(randomQuote);
                    stream.Write(data, 0, data.Length);

                    Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint} at {DateTime.Now}");
                    Console.WriteLine($"Sent quote: {randomQuote}");
                }
            }
        }
    }
}
