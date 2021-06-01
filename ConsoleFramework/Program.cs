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
            string[] strings = new string[20];
            List<SelectableTextList> l = new List<SelectableTextList>();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < strings.Length; j++)
                    strings[j] = ($"0");
                l.Add(new SelectableTextList(viewport, strings, 1, 1 + i, ConsoleColor.White, ConsoleColor.Black, new Enum[] { Options.Direction.Horizontal }));
            }

            l[5].Items[5].Active = true;
            InputHandler.WaitForInput();
        }
    }
}
