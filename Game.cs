using System;
using System.Collections.Generic;
namespace tcp_listeener
{
    class Game
    {
        public int turn;
        public bool findWinner;
        public char[,] fields = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        public void NewGame()
        {
            char[,] field = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
            fields = field;
        }
        public void CheckGameStatus(List<NetworkPlayer> players)
        {
            if(GameStatus() == '/')
            {
                players[0].writer.WriteLine("Draw");
                players[1].writer.WriteLine("Draw");
                findWinner = true;
            }
            if(GameStatus() == players[0].gameSymbol)
            {
                players[0].writer.WriteLine("{0} is winner", players[0].name);
                players[1].writer.WriteLine("{0} is winner", players[0].name);
                findWinner = true;
            }
            if(GameStatus() == players[1].gameSymbol)
            {
                players[0].writer.WriteLine("{0} is winner", players[1].name);
                players[1].writer.WriteLine("{0} is winner", players[1].name);
                findWinner = true;
            }
        }
        public char GameStatus()
            {
                int noEmpty = 0;
                if (fields[0, 0] != ' ' && fields[1, 1] == fields[2, 2] && fields[0, 0] == fields[1, 1])
                {
                    return fields[0, 0];
                }
                if (fields[0, 2] != ' ' && fields[1, 1] == fields[2, 0] && fields[0, 2] == fields[1, 1])
                {
                    return fields[0, 2];
                }
                for (int i = 0; i < fields.GetLength(0); i++)
                {
                    if (fields[i, 0] != ' ' && fields[i, 1] == fields[i, 2] && fields[i, 0] == fields[i, 1])
                    {

                        return fields[i, 0];
                    }
                    if (fields[0, i] != ' ' && fields[1, i] == fields[2, i] && fields[0, i] == fields[1, i])
                    {

                        return fields[0, i];
                    }
                    for (int j = 0; j < fields.GetLength(0); j++)
                    {
                        if (fields[i, j] != ' ')
                        {
                            noEmpty++;
                        }
                        else
                        {
                            j += 2;
                        }
                    }
                }
                if (noEmpty == 9 && !findWinner)
                {
                    return '/';
                }
                else
                {
                    return '|';
                }
            }
        public void WriteField(List<NetworkPlayer> players){
            string getField = "";
            for(int i = 0; i < fields.GetLength(0); i++){
                for(int j = 0; j < fields.GetLength(1); j++){
                    getField += j < 2 ? fields[i,j] + "|" : $"{fields[i,j]}";
                }
                if(i < 2)
                {
                    getField += Environment.NewLine + "-+-+-" + Environment.NewLine;
                }
            }
            players[0].writer.WriteLine($"{getField}");
            players[1].writer.WriteLine($"{getField}");
        }
    }
}