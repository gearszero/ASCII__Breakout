using System;

namespace ProjektBreakout
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Game game = new Game(56,56);
            
            do
            {
                game.userInput();
                while (game.start && !game.end)
                {
                    game.renderFrame();
                    game.userInput();
                    game.Update();
                }
            } while (!game.end);
        }
    }
}