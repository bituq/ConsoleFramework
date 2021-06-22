using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Viewport v = new Viewport(true);
            var input = new SelectableTextInput(v, "placeholder", 1, 1);
            input.SelectionBackground = ConsoleColor.Red;
            input.Active = true;
            InputHandler.WaitForInput();
        }
    }
}
