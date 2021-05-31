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
            var viewport2 = new Viewport();
            var test2 = new SelectableTextInstance(viewport2, "Goodbye, old world!", 1, 4);
            test2.SelectionBackground = ConsoleColor.Green;
            test2.Active = true;
            test2.LinkWindow(viewport);
            var test = new SelectableTextInstance(viewport, "Hello, world", 1, 1);
            test.SelectionBackground = ConsoleColor.Red;
            test.LinkWindow(viewport2);
            test.Active = true;

            viewport.SelectionOrder.Add(new List<ISelectable> { test });
            viewport2.SelectionOrder.Add(new List<ISelectable> { test2 });
            InputHandler.WaitForInput();
        }
    }
}
