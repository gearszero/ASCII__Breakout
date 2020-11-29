using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektBreakout
{
    class Sprite
    {
        public Coordinate position;
        public Vector vector;

        public int rows; //y
        public int cols; //x

        public char[,] data;

        public Sprite(Coordinate position, Vector vector, char[,] data)
        {
            this.position = position;
            this.vector = vector;
            this.data = data;

            rows = data.GetLength(0);
            cols = data.GetLength(1);
        }


        public Sprite(int y, int x, char[,] data)
        {
            position = new Coordinate(y, x);
            this.data = data;

            rows = data.GetLength(0);
            cols = data.GetLength(1);
        }

        public void draw(GameSpace screen)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    int yPosition = (int)position.y;
                    int xPosition = (int)position.x;

                    screen.gameArr[(screen.rows - yPosition) - 1 + y, xPosition + x] = data[y, x];
                }
            }
        }
    }
}