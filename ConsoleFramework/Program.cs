using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Viewport viewport = new Viewport();
            List<TextInstance> instances = new List<TextInstance>();
            for (int i = 0; i < 10; i++)
            {
                instances.Add(new TextInstance(viewport, "Instance " + i, 1, 1 + i));
                viewport.Draw();
            }
            for (int i = 0; i < 10; i++)
            {
                instances.Add(new TextInstance(viewport, "abc " + i, 5, 1 + i));
                viewport.Draw();
            }
            instances[0].Text = "";
            viewport.Draw();
            instances[3].Text = "whoops";
            viewport.Draw();
            viewport.Clean();
            Console.SetCursorPosition(0, 25);
        }
    }
}
