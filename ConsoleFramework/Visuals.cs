using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFramework.Essentials;
using ConsoleFramework.Structs;

namespace ConsoleFramework
{
    class TextInstance : Instance
    {
        string text;
        int x;
        int y;
        ConsoleColor foreground = ConsoleColor.White;
        ConsoleColor background = ConsoleColor.Black;

        public TextInstance(Viewport Viewport, string Text, int X, int Y, ConsoleColor Foreground, ConsoleColor Background) : base(Viewport)
        {
            x = X;
            y = Y;
            foreground = Foreground;
            background = Background;
            this.Text = Text;
        }
        public TextInstance(Viewport Viewport, string Text, int X, int Y) : base(Viewport)
        {
            x = X;
            y = Y;
            this.Text = Text;
        }

        public string Text
        {
            get => text;
            set
            {
                foreach (Cell cell in cache)
                    cell.Clean();
                for (int i = 0; i < value.Length; i++)
                    AddToCache(new Cell(value[i], x + i, y, foreground, background));
            }
        }
    }
}
