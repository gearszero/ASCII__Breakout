using System.Drawing;

namespace ProjektBreakout
{
    public class GamePoints
    {
        public int x;
        public int y;

        public int gamePointID;

        public Coordinate position;
        private Sprite wallPoint;
        public bool isVisible = true;

        public GamePoints(Coordinate position)
        {
            this.position = position;
        }

        public GamePoints(int y, int x, int gamePointID)
        {
            position = new Coordinate(y,x);
            this.gamePointID = gamePointID;
            fillWallPoint();
        }

        private void fillWallPoint()
        {
            wallPoint = new Sprite((int)position.y, (int)position.x, new char[1,3]{{'■','■','■'}});
        }

        public void draw(GameSpace screen)
        {
            for (int arrY = 0; arrY < 1; arrY++)
            {
                for (int arrX = 0; arrX < 3; arrX++)
                {
                    if (!isVisible)
                    {
                       wallPoint = new Sprite((int)position.y, (int)position.x, new char[1,3]{{' ',' ',' '}});
                    }
                    screen.gameArr[(int)position.y, (int)position.x + arrX] = wallPoint.data[arrY,arrX];
                }
            }
        }
    }
}