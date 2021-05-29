using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
                new Viewport();
            InputHandler.WaitForInput();
        }
    }
}
