using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ProjektBreakout
{
    class Game
    {
        public bool end { get; private set; }
        public bool start = false;

        public int rows;
        public int cols;

        //ball
        private Sprite ball;
        private Vector vectorBall;

        private float startingSpeedY = -0.06f;
        private float startingSpeedX = -0.06f;

        private float speedUp = 0.0008f;
        private float changeHardRight = 0.02f;
        private float changeHardLeft = -0.02f;

        //player
        private Sprite player;
        private Vector vectorPlayer;
        private int lives = 3;
        private int score = 0;

        //
        private GameSpace screen;
        private Sprite[] sprites;
        private GameScore gameScore;

        //platform
        private const char platform = '▀';
        private const int platformWidth = 11;

        //wallPoint
        private GamePoints wallPoint ;
        private GamePoints[] gamePoints;
        private int id = 0;

        //Testing
        private int notConstantPlatformWidth = 11;
        private bool testing = false;


        public Game( int rows, int cols)
        {
            this.end = false;
            this.rows = rows;
            this.cols = cols;
            
            screen = new GameSpace(rows, cols);
            vectorBall = new Vector(startingSpeedY, startingSpeedX);

            ball = new Sprite(30,5, new char[1, 1] { { '■' } });
            player = new Sprite((rows-1)/8, (cols-1)/2, new char[1, platformWidth] { { platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform } });

            fillWallPoints();
            sprites = new Sprite[] { ball, player};
        }

        //RENDRUJU SNIMKY
        public void renderFrame()
        {
            screen.deleteGameSpace();
            
            for (int k = 0; k < gamePoints.Length; k++)
            {
                gamePoints[k].draw(screen);
            }

            for (int p = 0; p < sprites.Length; p++)
            {
                sprites[p].draw(screen);
            }
            screen.showField();
        }
        
        //PLNIM MATICI WALLPOINTAMA
        public void fillWallPoints() 
        {
            int k = 0;
            int lenght = cols-2;
            int height = 10;
            int countOfRows = 8;
            int arrX = (lenght / 3);

            gamePoints = new GamePoints[countOfRows*arrX];
            for (int y = height; y < height+countOfRows; y++)
            {
               for (int x = 1; x < lenght; x+=3)
               {
                    wallPoint = new GamePoints(y,x,id);
                    gamePoints[k] = wallPoint;
                    k++;
               }
               id++;
            }
        }


        //USER INPUT
        public void userInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (player.position.x >= cols - platformWidth - 1)
                        {
                            vectorPlayer = new Vector(0, 0);
                        }
                        else
                            vectorPlayer = new Vector(0, 1f);
                        break;

                    case ConsoleKey.LeftArrow:
                        if (player.position.x < 2)
                        {
                            vectorPlayer = new Vector(0, 0);
                        }
                        else
                            vectorPlayer = new Vector(0, -1f);
                        break;
                    
                    //UKONCENI
                    case ConsoleKey.Escape:
                        end = true;
                        break;
                    
                    case ConsoleKey.Enter:
                        start = true;
                        break;
                    
                    //TESTING MODE
                    case ConsoleKey.P:
                        player = new Sprite((rows-1)/8, 1, new char[1, 54] { { platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform, platform } });
                        sprites = new Sprite[] { ball, player};
                        testing = true;
                        notConstantPlatformWidth = 54;
                        break;
                }
            }
        }

        public void Update()
        {
            Coordinate testPositionBall = ball.position += vectorBall;
            Coordinate testPlatformPosition = player.position += vectorPlayer;
            Console.WriteLine($"LIVES {lives}");
            Console.WriteLine($"SCORE {score}");
            // Console.WriteLine($"Yball = {(int)testPositionBall.y}");
            // Console.WriteLine($"Xball = {(int)testPositionBall.x}");
            // Console.WriteLine($"Yplat = {testPlatformPosition.y}");
            // Console.WriteLine($"XPlat = {testPlatformPosition.x}");
            // Console.WriteLine($"ROWS Y = {screen.rows}");
            // Console.WriteLine($"COLUMS X = {screen.cols}");
            // Console.WriteLine($"vecY = {vectorBall.y}");
            // Console.WriteLine($"vecX = {vectorBall.x}");
            // Console.WriteLine($"vecX = {gamePoints.Length}");

            changeDirection(testPositionBall, testPlatformPosition);

        }

        private void changeDirection(Coordinate testPositionBall, Coordinate testPlatformPosition)
            {
                //LEVY BOK
                if (testPositionBall.x < 2)
                {
                    //ZE SPODA NAHORU
                    if (vectorBall.y > 0 && vectorBall.x < 0)
                    {
                        vectorBall = new Vector(vectorBall.y, Math.Abs(vectorBall.x)); // Y+ X+
                    }
                    //Z VRCHU DOLU
                    else if (vectorBall.y < 0 && vectorBall.x < 0)
                    {
                        vectorBall = new Vector(vectorBall.y, Math.Abs(vectorBall.x)); //Y- X +
                    }
                }
                //PRAVY BOK
                if (testPositionBall.x >= screen.cols - 3)
                {
                    //ZE SPODA NAHORU
                    if (vectorBall.y > 0 && vectorBall.x > 0)
                    {
                        vectorBall = new Vector(vectorBall.y, -vectorBall.x); // Y+ X -
                    }
                    //Z VRCHU DOLU
                    else if (vectorBall.y < 0 && vectorBall.x > 0)
                    {
                        vectorBall = new Vector(vectorBall.y, -vectorBall.x); //Y - X -
                    }
                }

                //VRCH
                if (testPositionBall.y >= screen.rows - 2)
                {
                    //Z LEVA DOPRAVA
                    if (vectorBall.y > 0 && vectorBall.x > 0)
                    {
                        vectorBall = new Vector(-vectorBall.y, vectorBall.x); //Y- X +
                    }
                    //Z PRAVA DOLEVA
                    else if (vectorBall.y > 0 && vectorBall.x < 0)
                    {
                        vectorBall = new Vector(-vectorBall.y, vectorBall.x); //Y- X -
                    }
                }

                //SPODEK
                if (testPositionBall.y < 2)
                {
                    //POKUD SPADNE MICEK NASTAVI SE SEM
                    ball.position = new Coordinate((screen.rows - 1) - ((screen.rows - 1) / 2), (screen.cols - 1) - ((screen.cols - 1) / 2));
                    vectorBall = new Vector(startingSpeedY, -startingSpeedX);
                    lives--;
                    start = false;
                    if (lives == 0)
                    {
                        end = true;
                    }
                }
                

                //DOTEK PLATFORMY
                int ballPositionXint = (int) testPositionBall.x;
                int ballPositionYint = (int) testPositionBall.y;
                int middleOfPlatform = platformWidth / 2;
                
                if (!testing)//pokud neni zaply testing
                {
                    if (ballPositionYint <= (rows - 1) / 8)
                    {
                        //KDE MICEK TREFIL PLATFORMU A JESTLI VUBEC
                        for (int i = 0; i < platformWidth; i++)
                        {
                            //KDE PRESNE SE TREFIL
                            int platformPositionXint = (int) testPlatformPosition.x + i;

                            if (((rows - 1) / 8 == ballPositionYint) && platformPositionXint == ballPositionXint)
                            {
                                speedUpBall(i, middleOfPlatform);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    testingGame(ballPositionYint,ballPositionXint);
                }


                //Dotek wallpointu
                
                if (ballPositionYint > rows - 20)
                {
                    for (int i = 0; i < gamePoints.Length; i++)
                    {
                        int positionOfY = rows - (int)gamePoints[i].position.y;
                        int positionOfX = (int) gamePoints[i].position.x;
                        //Kontrola i jinych pozic x
                        for (int p = 0; p < 3; p++)
                        {
                            //Micek se dotkne ze spodu wallpointu
                            if ((ballPositionXint == positionOfX+p) && ((int)testPositionBall.y == positionOfY-1 || ballPositionYint == positionOfY) && (gamePoints[i].isVisible == true) && (vectorBall.y > 0))
                            {
                                gamePoints[i].isVisible = false;
                                speedUpBallFromWall(gamePoints[i].gamePointID);
                                addScore(gamePoints[i].gamePointID);
                                break;
                            }
                            //Micek se dotkne z vrchu wallpointu
                            else if ((ballPositionXint == positionOfX+p) && ((int)testPositionBall.y == positionOfY+1) && (gamePoints[i].isVisible == true) && (vectorBall.y < 0))
                            {
                                gamePoints[i].isVisible = false;
                                speedUpBallFromWall(gamePoints[i].gamePointID);
                                addScore(gamePoints[i].gamePointID);
                                break;
                            }
                        }
                    }
                }
                ball.position += vectorBall;
                player.position += vectorPlayer;
                vectorPlayer = new Vector(0, 0);
            }

        //ZRYCHLOVANI MICE
        private void speedUpBall(int placeOfHit, int middle)
        {
            //MICEK TREFIL Z LEVA A JDE DOPRAVA
            if (vectorBall.y < 0 && vectorBall.x >= 0)
            {
                //POKUD DOPADNE NA STRED
                if (placeOfHit == middle || placeOfHit == middle - 1)
                {
                    vectorBall = new Vector(Math.Abs(vectorBall.y), vectorBall.x - speedUp*2); // Y+ X-
                }
                //POKUD DOPADL NA LEVOU CAST PLATFORMY, TAK SE ODRAZIL DO STEJNEHO SMERU
                else if (placeOfHit < middle - (middle/3))
                {
                    vectorBall.x = changeHardLeft - vectorBall.x;
                    vectorBall.y = -vectorBall.y;
                }

                else
                {
                    vectorBall = new Vector(Math.Abs(vectorBall.y), vectorBall.x); // Y+ X+   
                }
            }
            //MICEK TREFIL Z PRAVA DLOEVA
            else if (vectorBall.y < 0 && vectorBall.x < 0)
            {
                //POKUD DOPADNE NA STRED
                if (placeOfHit == middle || placeOfHit == middle - 1)
                {
                    vectorBall = new Vector(Math.Abs(vectorBall.y), vectorBall.x + speedUp*2);
                }
                //POKUD DOPADL NA PRAVOU CAST PLATFORMY, TAK SE ODRAZIL DO STEJNEHO SMERU
                else if (placeOfHit > middle + (middle/3))
                {
                    vectorBall.x = changeHardRight + vectorBall.x;
                    vectorBall.y = -vectorBall.y;
                }

                else
                {
                    vectorBall = new Vector(Math.Abs(vectorBall.y), vectorBall.x); // Y+ X-
                }
            }
        }
        
        //ZRYCHLENI MICKU OD WALLPOINT
        private void speedUpBallFromWall(int passId)
        {
            //POCITANI ZRYCHLENI
            float speedUpFromWall = speedUp;
            passId = id - passId;
            float idFloat = Convert.ToSingle(passId);
            speedUpFromWall = idFloat / 5000 + speedUp*3;


            //Z LEVA DOPRAVA
            if (vectorBall.y > 0 && vectorBall.x > 0)
            {
                vectorBall = new Vector(-vectorBall.y - speedUpFromWall, vectorBall.x + speedUpFromWall);
            }
            //Z PRAVA DOLEVA
            else if (vectorBall.y > 0 && vectorBall.x < 0)
            {
                vectorBall = new Vector(-vectorBall.y - speedUpFromWall, vectorBall.x - speedUpFromWall);
            }
            //VRCH Z LEVA DOPRAVA
            else if (vectorBall.y < 0 && vectorBall.x > 0)
            {
                vectorBall = new Vector(-vectorBall.y + speedUpFromWall, vectorBall.x + speedUpFromWall);
            }
            //VRCH Z PRAVA DOLEVA
            else if (vectorBall.y < 0 && vectorBall.x < 0)
            {
                vectorBall = new Vector(-vectorBall.y + speedUpFromWall, vectorBall.x - speedUpFromWall);
            }
        }
        //PRIDAVANI SKORE
        private void addScore(int passId)
        {
            passId = id - passId + 1;
            score += passId;
        }

        //TESTOVACI NASTAVENI PLATFORMY
        private void testingGame(int ballPositionYint, int ballPositionXint)
        {
            if (ballPositionYint <= (rows - 1) / 8)
            {
                if (vectorBall.y < 0 && vectorBall.x >= 0)
                {
                    vectorBall = new Vector(Math.Abs(vectorBall.y), vectorBall.x); // Y+ X+   
                }
                //MICEK TREFIL Z PRAVA DLOEVA
                else if (vectorBall.y < 0 && vectorBall.x < 0)
                {
                    vectorBall = new Vector(Math.Abs(vectorBall.y), vectorBall.x); // Y+ X-
                }
            }
        }
    }
}
