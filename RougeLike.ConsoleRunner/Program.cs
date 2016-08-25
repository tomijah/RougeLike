namespace RougeLike.ConsoleRunner
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using RougeLike.Game;

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            game.Run(60);
        }
    }
}
