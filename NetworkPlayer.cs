using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
namespace tcp_listeener
{
    internal class NetworkPlayer
    {
        TcpClient client;
        string name;
        char gameSymbol;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        public NetworkPlayer(TcpClient client)
        {
            this.client = client;
        }
        public void StartReading()
        {
            stream = client.GetStream();
            this.reader = new StreamReader(stream);
        }
        public void AskName()
        {
            StartReading();
            Console.WriteLine("Enter your name:");
            this.name = reader.ReadLine();
        }
        public void AskChar()
        {
            StartReading();
            Console.WriteLine("Enter your char:");
            this.name = reader.ReadLine();
        }
        public void Turn(char[,] fields)
        {
            StartReading();
            string takeCord = reader.ReadLine();
            takeCord.Split(" ");
            int x = Convert.ToInt32(takeCord[0]) - 1;
            int y = Convert.ToInt32(takeCord[1]) - 1;
            if (fields[x, y] == ' ')
            {
                fields[x, y] = gameSymbol;
            }
            else
            {
                Console.WriteLine("Invalid value");
                Turn(fields);
            }
        }
    }
}