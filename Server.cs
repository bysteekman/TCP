using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace tcp_listeener
{

    class Server
    {
        public static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                int turn = 0;
                bool findWinner = false;
                // Set the TcpListener on port 13000.
                IPAddress localAddr = IPAddress.Parse(args[0]);
                Int32 port = Int32.Parse(args[1]);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    List<NetworkPlayer> players = new List<NetworkPlayer>();

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    while(players.Count != 2)
                    {
                        Console.Write("Waiting for a connection... ");
                        TcpClient client = server.AcceptTcpClient();
                        NetworkPlayer player = new NetworkPlayer(client);
                        player.AskName();
                        player.AskChar();
                        Console.WriteLine("Connected!");
                    }

                    data = null;

                    // Loop to receive all the data sent by the client.
                    while (turn <= 9 && findWinner != true)
                    {                        
                       Console
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}