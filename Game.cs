using System;
namespace 
class Game
    {
        public char firstPlayer;
        public char secondPlayer;
        public int turn;
        public bool findWinner;
        private char[,] fields = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
        public char GameStatus()
        {
            int noEmpty = 0;
            if (fields[0, 0] != ' ' && fields[1, 1] == fields[2, 2] && fields[0, 0] == fields[1, 1])
            {
                findWinner = true;
                return fields[0, 0];
            }
            if (fields[0, 2] != ' ' && fields[1, 1] == fields[2, 0] && fields[0, 2] == fields[1, 1])
            {
                findWinner = true;
                return fields[0, 2];
            }
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                if (fields[i, 0] != ' ' && fields[i, 1] == fields[i, 2] && fields[i, 0] == fields[i, 1])
                {
                    findWinner = true;
                    return fields[i, 0];
                }
                if (fields[0, i] != ' ' && fields[1, i] == fields[2, i] && fields[0, i] == fields[1, i])
                {
                    findWinner = true;
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
            if (noEmpty != 9 && findWinner == false)
            {
                return turn % 2 == 0 ? 'X' : 'O';
            }
            if (noEmpty == 9)
            {
                return '/';
            }
            else
            {
                return '*';
            }
        }
        public char Turn((int, int) coords)
        {
            int rt = turn;
            turn++;
            var (x, y) = coords;
            if (fields[x, y] == ' ')
            {
                return rt % 2 == 0 ? fields[x, y] = firstPlayer : fields[x, y] = secondPlayer;
            }
            else
            {
                if (findWinner != true)
                {
                    turn--;
                    return '-';
                }
                else { return '!'; }
            }
        }
        static void writeField(char[,] fild)
        {
            for (int i = 0; i < fild.GetLength(0); i++)
            {
                for (int j = 0; j < fild.GetLength(1); j++)
                {
                    Console.Write(j < 2 ? fild[i, j] + "|" : fild[i, j]);
                }
                Console.Write(i < 2 ? "\n" + new string('-', 5) + "\n" : "\n");
            }
        }
        static (int, int) TakeCoord()
        {
            string[] takeCord = reader;
            if (takeCord.Length == 2)
            {
                int x = Convert.ToInt32(takeCord[0]) - 1;
                int y = Convert.ToInt32(takeCord[1]) - 1;
                return (x, y);
            }
            else
            {
                Console.WriteLine("Invalid value");
                return TakeCoord();
            }
        }
    }