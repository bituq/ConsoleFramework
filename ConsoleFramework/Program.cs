using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            var viewport = new Viewport(true);
            var button = new SelectableTextInstance(viewport, "Button", 1, 5, ConsoleColor.Yellow);
            var explanation = new TextInstance(viewport, "Please click the button below.", 1, 3, ConsoleColor.White, ConsoleColor.Black);
            int number = 0;
            var counter = new TextInstance(viewport, $"Counter: {number}", 1, 6, ConsoleColor.DarkGray, ConsoleColor.Black);
            button.KeyActionPairs[ConsoleKey.Enter] = () =>
            {
                number++;
                counter.Text = $"Counter: {number}";
            };

            button.SelectionBackground = ConsoleColor.DarkRed;
            button.Active = true;

            InputHandler.WaitForInput();
        }
    }
}
