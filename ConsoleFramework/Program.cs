using System;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Viewport viewport = new Viewport();
            TextInstance t = new TextInstance(viewport, "How are you on this day?", 1, 1);
            viewport.Draw();
            t.Text = "instance1";
            TextInstance b = new TextInstance(viewport, "instance2", 11, 1);
            viewport.Draw();
            Console.SetCursorPosition(0, 25);
        }
    }
}
