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
        public TcpClient client;
        public string name;
        public char gameSymbol;
        internal NetworkStream stream;
        internal StreamReader reader;
        internal StreamWriter writer;
        public NetworkPlayer(TcpClient client)
        {
            this.client = client;
        }
        public void StartStream()
        {
            stream = client.GetStream();
            this.reader = new StreamReader(stream);
            this.writer = new StreamWriter(stream);
            writer.AutoFlush = true;
        }
        public void AskName()
        {
            writer.WriteLine("Enter your name:");
            this.name = reader.ReadLine();
        }
        public void AskChar()
        {
            writer.WriteLine("Enter your char:");
            char[] readed = reader.ReadLine().ToCharArray();
            this.gameSymbol = readed[0];
        }
        public void MakeTurn(char[,] fields)
        {
                    writer.WriteLine("Your turn:");
                    Turn(fields);
        }
        public void EnemyTurn()
        {
            writer.WriteLine("Enemy turn");
        }
        public void Turn(char[,] fields)
        {
            string takeCord = reader.ReadLine();
            string[] nice = takeCord.Split(" ");
            int x = Convert.ToInt32(nice[0]) - 1;
            int y = Convert.ToInt32(nice[1]) - 1;
            if (fields[x, y] == ' ')
            {
                fields[x, y] = gameSymbol;
            }
            else
            {
                writer.WriteLine("Invalid value");
                Turn(fields);
            }
        }
    }
}