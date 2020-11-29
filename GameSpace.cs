using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektBreakout
{
    public class GameSpace
    {
        public int rows; //radek Y
        public int cols; //sloupec X

        public char[,] gameArr;

        private const char background = ' ';

        public GameSpace(int rows, int cols) {
            this.rows = rows;
            this.cols = cols;

            //Inicializuji si matici pole hry
            gameArr = new char[rows, cols];

            fillField();
            showField();
        }

        public void deleteGameSpace() 
        {
            for (int y = 1; y < rows-1; y++) 
            {
                for (int x = 1; x < cols-1; x++)
                {
                    gameArr[y, x] = background;
                }
            }
        }

        public void fillField() 
        {
            const char wall = '█';

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (y == 0 || y == (rows - 1))
                    {
                        gameArr[y, x] = wall;
                    }
                    else if (y == (rows - 1) - ((rows-1)/8) && (x == 0 || x == cols-1))
                    {
                        gameArr[y, x] = '▀';
                    }
                    else if (x == 0 || x == (cols - 1))
                    {
                        gameArr[y, x] = wall;
                    }
                    else
                    {
                        gameArr[y, x] = background;
                    }
                }
            }
        }

        public void showField()
        {
            Console.SetWindowSize(rows, cols+5);
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < rows; y++)
            {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < cols; x++)
                {
                    sb.Append(gameArr[y, x]);
                }

                switch (y)
                {
                    case 10:
                    case 11:
                        Console.ForegroundColor = ConsoleColor.Red; 
                        Console.WriteLine(sb);
                        break;

                    case 12:
                    case 13:
                        Console.ForegroundColor = ConsoleColor.Yellow; 
                        Console.WriteLine(sb);
                        break;
                    case 14:
                    case 15:
                        Console.ForegroundColor = ConsoleColor.Blue; 
                        Console.WriteLine(sb);
                        break;
                    case 16:
                    case 17:
                        Console.ForegroundColor = ConsoleColor.Green; 
                        Console.WriteLine(sb);
                        break;
                    case 49: 
                        Console.ForegroundColor = ConsoleColor.Cyan; 
                        Console.WriteLine(sb);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.White; 
                        Console.WriteLine(sb);
                        break;
                }
                
            }
        }
    }
}