using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TextList> l = new List<TextList>();
            var viewport = new Viewport(true);
            int current = 33;
            for (int j = 0; j < 7; j++)
            {
                List<string> list = new List<string>();
                for (int i = 0; i < 15; i++)
                     list.Add($"[{current}]{(char)current++}");
                l.Add(new TextList(viewport, list.ToArray(), 0, j, ConsoleColor.White, ConsoleColor.Black, new Enum[] { Options.Direction.Horizontal }));
            }
            InputHandler.WaitForInput();
        }
    }
}
