using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFramework.Essentials;
using ConsoleFramework.Data;
using ConsoleFramework.Utils;

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

        public override string ToString() => Text;
    }

    interface ICollection<T>
    { 
        IReadOnlyList<T> Items { get; }
    }

    internal class TextInstanceList<T> : ICollection<T> where T : TextInstance
    {
        List<Enum> properties;
        List<string> strings;
        protected List<T> items = new List<T>();
        protected Viewport viewport;
        int spacing = 2;
        int x;
        int y;
        ConsoleColor foreground;
        ConsoleColor background;
        protected int len = 0;

        public TextInstanceList(Viewport Viewport, string[] Items, int X, int Y, ConsoleColor Foreground, ConsoleColor Background, IEnumerable<Enum> Properties)
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

        protected int xIncrement(int index)
        {
            if (properties.Contains(Options.Direction.Horizontal))
            {
                len += (items.Count == 0 ? 0 : items[index - 1].Text.Length - 1);
                return x + index * spacing + len;
            }
            return x;
        }
        protected int yIncrement(int index) => properties.Contains(Options.Direction.Vertical) ? y + (index * spacing) : y;

        public IReadOnlyList<T> Items { get => items; }

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

        protected virtual void set()
        {
        }
    }

    class TextList : TextInstanceList<TextInstance>
    {

        public TextList(Viewport Viewport, string[] Items, int X, int Y, ConsoleColor Foreground, ConsoleColor Background, IEnumerable<Enum> Properties)
            : base(Viewport, Items, X, Y, Foreground, Background, Properties) { }
        protected override void set()
        {
            foreach (TextInstance item in items)
            {
                item.Clean();
                viewport.RemoveInstance(item);
            }
            items.Clear();
            for (int i = 0; i < Strings.Count; i++)
                items.Add(new TextInstance(viewport, Strings[i], xIncrement(i), yIncrement(i), Foreground, Background));
            len = 0;
        }
    }

    class SelectableTextList : TextInstanceList<SelectableTextInstance>
    {
        public ConsoleColor SelectedForeground = ConsoleColor.Black;
        public ConsoleColor SelectedBackground = ConsoleColor.White;
        public SelectableTextList(Viewport Viewport, string[] Items, int X, int Y, ConsoleColor Foreground, ConsoleColor Background, IEnumerable<Enum> Properties)
            : base(Viewport, Items, X, Y, Foreground, Background, Properties) { }
        protected override void set()
        {
            foreach (SelectableTextInstance item in items)
            {
                item.Clean();
                viewport.RemoveInstance(item);
            }
            items.Clear();
            for (int i = 0; i < Strings.Count; i++)
            {
                var Item = new SelectableTextInstance(viewport, Strings[i], xIncrement(i), yIncrement(i), Foreground, Background);
                items.Add(Item);
                Item.SelectionForeground = SelectedForeground;
                Item.SelectionBackground = SelectedBackground;
            }
            len = 0;
        }
    }

    interface ISelectable
    {
        public Dictionary<ConsoleKey, Action> KeyActionPairs { get; set; }

        public bool Active { get; set; }

        public bool TryAction(ConsoleKey Key);
    }

    class SelectableTextInstance : TextInstance, ISelectable
    {
        public Dictionary<SelectableTextInstance, float> Distances { get; private set; }
        Tuple<ConsoleColor, ConsoleColor> defaultColors;
        public ConsoleColor SelectionForeground;
        public ConsoleColor SelectionBackground;
        int searchRange = 64;
        bool active = false;
        public SelectableTextInstance(Viewport Viewport, string Text, int X, int Y, ConsoleColor Foreground = ConsoleColor.White, ConsoleColor Background = ConsoleColor.Black) : base(Viewport, Text, X, Y, Foreground, Background)
        {
            defaultColors = Tuple.Create(Foreground, Background);
            SelectionForeground = Foreground;
            SelectionBackground = Background;
            KeyActionPairs[ConsoleKey.Enter] = () =>
            {
                if (Link != null)
                    Link.Active = true;
            };

            KeyActionPairs[ConsoleKey.UpArrow] = () => MakeActive(north);
            KeyActionPairs[ConsoleKey.RightArrow] = () => MakeActive(east);
            KeyActionPairs[ConsoleKey.DownArrow] = () => MakeActive(south);
            KeyActionPairs[ConsoleKey.LeftArrow] = () => MakeActive(west);

        }

        public Viewport Link { get; private set; }
        public bool Active
        {
            get => active;
            set
            {
                active = value;
                if (value)
                {
                    Foreground = SelectionForeground;
                    Background = SelectionBackground;
                    if (viewport.ActiveSelectable != null)
                        viewport.ActiveSelectable.Active = false;
                    viewport.ActiveSelectable = this;
                }
                else
                {
                    Foreground = defaultColors.Item1;
                    Background = defaultColors.Item2;
                }
            }
        }
        public Dictionary<ConsoleKey, Action> KeyActionPairs { get; set; } = new Dictionary<ConsoleKey, Action>();

        public bool TryAction(ConsoleKey Key)
        {
            Action action;
            if (KeyActionPairs.TryGetValue(Key, out action))
                action();
            return action != null;
        }

        public void LinkWindow(Viewport Viewport) => Link = Viewport;

        public void CalculateDistances(bool debug = false)
        {
            Distances = new Dictionary<SelectableTextInstance, float>();
            foreach (Instance Instance in viewport.Instances)
                if (!Instance.Equals(this) && Instance.GetType() == this.GetType())
                {
                    SelectableTextInstance Selectable = Instance as SelectableTextInstance;
                    float Distance = Calc.Distance(X, Y, Selectable.X, Selectable.Y);
                    if (Distance < searchRange)
                    {
                        Distances.Add(Selectable, Distance);
                        if (debug)
                        {
                            if (Distance >= 30)
                                Selectable.Background = ConsoleColor.DarkBlue;
                            else if (Distance >= 25)
                                Selectable.Background = ConsoleColor.DarkGreen;
                            else if (Distance >= 20)
                                Selectable.Background = ConsoleColor.Green;
                            else if (Distance >= 15)
                                Selectable.Background = ConsoleColor.Yellow;
                            else if (Distance >= 10)
                                Selectable.Background = ConsoleColor.DarkYellow;
                            else if (Distance >= 5)
                                Selectable.Background = ConsoleColor.DarkRed;
                            else if (Distance < 5)
                                Selectable.Background = ConsoleColor.Red;
                        }
                    }
                }
        }

        void MakeActive(Func<int, int, int, int, bool> Direction)
        {
            if (FindClosest(Direction) is SelectableTextInstance n && n != null) n.Active = true;
        }
        bool south(int X1, int Y1, int X2, int Y2) => Y2 > Y1;
        bool east(int X1, int Y1, int X2, int Y2) => X2 > X1;
        bool north(int X1, int Y1, int X2, int Y2) => Y2 < Y1;
        bool west(int X1, int Y1, int X2, int Y2) => X2 < X1;

        SelectableTextInstance FindClosest(Func<int, int, int, int, bool> f)
        {
            double min = double.MaxValue;
            SelectableTextInstance res = null;
            foreach (SelectableTextInstance Instance in Distances.Keys)
                if (f(X, Y, Instance.X, Instance.Y) && Distances[Instance] < min)
                {
                    min = Distances[Instance];
                    res = Instance;
                }
            return res;
        }
    }
}
