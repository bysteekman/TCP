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
            Game game = new Game();
            try
            {
                // Set the TcpListener on port 13000.
                IPAddress localAddr = IPAddress.Parse(args[0]);
                Int32 port = Int32.Parse(args[1]);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();


                // Enter the listening loop.
                while (true)
                {
                    List<NetworkPlayer> players = new List<NetworkPlayer>();
                    game.turn = 0;
                    game.findWinner = false;

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    while (players.Count != 2)
                    {
                        Console.Write("Waiting for a connection... ");
                        TcpClient client = server.AcceptTcpClient();
                        NetworkPlayer player = new NetworkPlayer(client);
                        player.StartStream();
                        player.AskName();
                        player.AskChar();
                        players.Add(player);
                        Console.WriteLine("Connected!");
                    }


                    // Loop to receive all the data sent by the client.
                    while (!game.findWinner)
                    {
                        var (active, waiting) = game.turn % 2 == 0 ? (0, 1) : (1, 0);
                        game.WriteField(players);
                        players[waiting].EnemyTurn();
                        game.CheckGameStatus(players);
                        players[active].MakeTurn(game.fields);
                        game.CheckGameStatus(players);
                        game.turn++;
                    }

                    foreach (NetworkPlayer player in players)
                    {
                        player.client.Close();
                    }
                    game.NewGame();
                    game.findWinner = false;
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