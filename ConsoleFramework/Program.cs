using System;
using System.Collections.Generic;
using ConsoleFramework.Essentials;
using ConsoleFramework.Builder;

namespace ConsoleFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Viewport v = new Viewport(true);
            v.Initializer = () =>
            {
                var h = new SelectableTextInput(v, "hello", 1, 1);
                new SelectableTextInput(v, "bye", 1, 2);
                var e = new SelectableTextInstance(v, "Button", 1, 3);
                e.SelectionBackground = ConsoleColor.Red;
                h.Active = true;
            };
            InputHandler.WaitForInput();
        }
    }
}
