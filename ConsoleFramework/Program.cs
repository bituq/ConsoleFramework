using System;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Viewport viewport = new Viewport();
            TextInstance t = new TextInstance(viewport, "Hello, world!", 1, 1);
            viewport.Draw();
            t.Text = "How are you today?";
            viewport.Draw();
            Console.SetCursorPosition(0, 25);
        }
    }
}
