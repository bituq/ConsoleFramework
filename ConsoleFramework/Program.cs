using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //Viewport viewport = new Viewport();
            List<TextList> a = new List<TextList>();
            string[] t = new string[26];
                for (int i = 0; i < t.Length; i++)
                    t[i] = i.ToString();
                //a.Add(new TextList(viewport, t, 1, 1, ConsoleColor.White, ConsoleColor.Black, new Enum[] { Options.Direction.Horizontal }));
            //var temp = new List<string>(a[0].Strings);
            //for (int i = 0; i < temp.Count; i++)
                //temp[i] = (char)(65 + i) + temp[i].ToString();
            InputHandler.WaitForInput();
            Console.SetCursorPosition(0, 35);
        }
    }
}
