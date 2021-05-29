using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFramework.Essentials;
using ConsoleFramework.Data;

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
                Clean();
                for (int i = 0; i < value.Length; i++)
                    AddToCache(new Cell(value[i], x + i, y, foreground, background));
                text = value;
            }
        }

        public int X
        {
            get => x;
            set
            {
                x = value;
                Text = text;
            }
        }
        public int Y
        {
            get => y;
            set
            {
                y = value;
                Text = text;
            }
        }
        public ConsoleColor Foreground
        {
            get => foreground;
            set
            {
                foreground = value;
                Text = text;
            }
        }
        public ConsoleColor Background
        {
            get => background;
            set
            {
                background = value;
                Text = text;
            }
        }

        public TextInstance Copy() => new TextInstance(viewport, Text, X, Y, Foreground, Background);
        public TextInstance Copy(int x, int y) => new TextInstance(viewport, Text, x, y, Foreground, Background);

        public void Clean()
        {
            foreach (Cell cell in cache)
            {
                cell.Clean();
                garbageCollector.Add(cell);
            }
        }
    }

    interface ICollection<T>
    { 
        IReadOnlyList<T> Items { get; }
    }

    partial class TextList : ICollection<TextInstance>
    {
        List<Enum> properties;
        List<string> strings;
        List<TextInstance> items = new List<TextInstance>();
        Viewport viewport;
        int spacing = 2;
        int x;
        int y;
        ConsoleColor foreground;
        ConsoleColor background;
        int len = 0;

        public TextList(Viewport Viewport, string[] Items, int X, int Y, ConsoleColor Foreground, ConsoleColor Background, IEnumerable<Enum> Properties)
        {
            strings = new List<string>(Items);
            properties = new List<Enum>(Properties);
            viewport = Viewport;
            x = X;
            y = Y;
            foreground = Foreground;
            background = Background;
            set();
        }

        int xIncrement(int index)
        {
            if (properties.Contains(Options.Direction.Horizontal))
            {
                len += (items.Count == 0 ? 0 : items[index - 1].Text.Length - 1);
                return x + index * spacing + len;
            }
            return x;
        }
        int yIncrement(int index) => properties.Contains(Options.Direction.Vertical) ? y + (index * spacing) : y;

        public IReadOnlyList<TextInstance> Items { get => items; }

        public List<string> Strings
        {
            get => strings;
            set
            {
                strings = value;
                set();
            }
        }

        public int X
        {
            get => x;
            set
            {
                x = value;
                set();
            }
        }

        public int Y
        {
            get => y;
            set
            {
                y = value;
                set();
            }
        }

        public ConsoleColor Foreground
        {
            get => foreground;
            set
            {
                foreground = value;
                set();
            }
        }

        public ConsoleColor Background
        {
            get => background;
            set
            {
                background = value;
                set();
            }
        }

        public int Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                set();
            }
        }
        
        public List<Enum> Properties
        {
            get => properties;
            set
            {
                properties = value;
                set();
            }
        }

        void set()
        {
            foreach (TextInstance item in items)
            {
                item.Clean();
                viewport.RemoveInstance(item);
            }
            items.Clear();
            for (int i = 0; i < strings.Count; i++)
                items.Add(new TextInstance(viewport, strings[i], xIncrement(i), yIncrement(i), foreground, background));
            len = 0;
        }
    }
}
