using System;
using System.Collections.Generic;
using System.Text;
using ConsoleFramework.Essentials;

namespace ConsoleFramework.Builder
{
    public class TextInputBuilder
    {
        private Tuple<Viewport, string, int, int, ConsoleColor, ConsoleColor> _params;
        private ConsoleColor _selectionForeground = ConsoleColor.White;
        private ConsoleColor _selectionBackground = ConsoleColor.Black;
        private bool _active = false;

        public TextInputBuilder(Viewport viewport, int x = 0, int y = 0)
        {
            this._params = Tuple.Create(viewport, "", x, y, ConsoleColor.White, ConsoleColor.Black);
        }

        public TextInputBuilder Position(int x, int y)
        {
            this._params = Tuple.Create(_params.Item1, _params.Item2, x, y, _params.Item5, _params.Item6);
            return this;
        }

        public TextInputBuilder Text(string text)
        {
            this._params = Tuple.Create(_params.Item1, text, _params.Item3, _params.Item4, _params.Item5, _params.Item6);
            return this;
        }

        public TextInputBuilder Color(ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            this._params = Tuple.Create(_params.Item1, _params.Item2, _params.Item3, _params.Item4, foreground, background);
            return this;
        }

        public TextInputBuilder Selected(ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            _selectionBackground = background;
            _selectionForeground = foreground;
            return this;
        }

        public TextInputBuilder Active()
        {
            _active = true;
            return this;
        }

        public SelectableTextInput Result()
        {
            var result = new SelectableTextInput(_params.Item1, _params.Item2, _params.Item3, _params.Item4, _params.Item5, _params.Item6);
            result.SelectionForeground = _selectionForeground;
            result.SelectionBackground = _selectionBackground;
            result.Active = _active;
            return result;
        }
    }

    public class TextListBuilder
    {
        private Tuple<Viewport, List<string>, int, int, ConsoleColor, ConsoleColor, List<Enum>> _params;
        private ConsoleColor _selectionForeground = ConsoleColor.White;
        private ConsoleColor _selectionBackground = ConsoleColor.Black;
        private int _spacing = 1;
        private bool _asSelectable = false;
        private bool _asTextInput = false;

        public TextListBuilder(Viewport viewport, int x = 0, int y = 0)
        {
            _params = Tuple.Create(viewport, new List<string>(), x, y, ConsoleColor.White, ConsoleColor.Black, new List<Enum>());
        }

        public TextListBuilder Position(int x, int y)
        {
            this._params = Tuple.Create(_params.Item1, _params.Item2, x, y, _params.Item5, _params.Item6, _params.Item7);
            return this;
        }

        public TextListBuilder Color(ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            this._params = Tuple.Create(_params.Item1, _params.Item2, _params.Item3, _params.Item4, foreground, background, _params.Item7);
            return this;
        }

        public TextListBuilder Direction(Options.Direction direction)
        {
            _params.Item7.Add(direction);
            return this;
        }

        public TextListBuilder SetItems(params string[] items)
        {
            this._params = Tuple.Create(_params.Item1, new List<string>(items), _params.Item3, _params.Item4, _params.Item5, _params.Item6, _params.Item7);
            return this;
        }

        public TextListBuilder Spacing(int spacing)
        {
            _spacing = spacing;
            return this;
        }

        public SelectableListBuilder AsSelectable(ConsoleColor foreground = ConsoleColor.Black, ConsoleColor background = ConsoleColor.White)
        {
            var result = new SelectableListBuilder(_params.Item1, _params.Item2.ToArray(), _params.Item3, _params.Item4, _params.Item5, _params.Item6, _params.Item7);
            result._selectionBackground = background;
            result._selectionForeground = foreground;
            result._spacing = _spacing;
            return result;
        }

        public TextList Result()
        {
            var result = new TextList(_params.Item1, _params.Item2.ToArray(), _params.Item3, _params.Item4, _params.Item5, _params.Item6, _params.Item7);
            result.Spacing = _spacing;
            return result;
        }

    }

    public class SelectableListBuilder
    {
        Tuple<Viewport, string[], int, int, ConsoleColor, ConsoleColor, List<Enum>> _params;
        internal ConsoleColor _selectionForeground;
        internal ConsoleColor _selectionBackground;
        internal int _spacing;
        bool _active = false;

        internal SelectableListBuilder(Viewport viewport, string[] items, int x, int y, ConsoleColor foreground, ConsoleColor background, List<Enum> properties)
        {
            _params = Tuple.Create(viewport, items, x, y, foreground, background, properties);
            _selectionBackground = ConsoleColor.White;
            _selectionForeground = ConsoleColor.Black;
        }

        public SelectableListBuilder Selected(ConsoleColor foreground = ConsoleColor.Black, ConsoleColor background = ConsoleColor.White)
        {
            _selectionBackground = background;
            _selectionForeground = foreground;
            return this;
        }

        public SelectableListBuilder Active()
        {
            _active = true;
            return this;
        }

        public TextInputListBuilder AsInput()
        {
            var result = new TextInputListBuilder(_params.Item1, _params.Item2, _params.Item3, _params.Item4, _params.Item5, _params.Item6, _params.Item7);
            result._selectionBackground = _selectionBackground;
            result._selectionForeground = _selectionForeground;
            return result;
        }

        public SelectableTextList Result()
        {
            var result = new SelectableTextList(_params.Item1, _params.Item2, _params.Item3, _params.Item4, _params.Item5, _params.Item6, _params.Item7);
            result.SelectedBackground = _selectionBackground;
            result.SelectedForeground = _selectionForeground;
            result.Spacing = _spacing;
            result[0].Active = _active;
            return result;
        }
    }

    public class TextInputListBuilder
    {
        Tuple<Viewport, string[], int, int, ConsoleColor, ConsoleColor, List<Enum>> _params;
        internal ConsoleColor _selectionBackground;
        internal ConsoleColor _selectionForeground;
        internal int _spacing;
        internal TextInputListBuilder(Viewport viewport, string[] items, int x, int y, ConsoleColor foreground, ConsoleColor background, List<Enum> properties)
        {
            _params = Tuple.Create(viewport, items, x, y, foreground, background, properties);
            _selectionBackground = ConsoleColor.White;
            _selectionForeground = ConsoleColor.Black;
        }
        
        public TextInputList Result()
        {
            var result = new TextInputList(_params.Item1, _params.Item2, _params.Item3, _params.Item4, _params.Item5, _params.Item6, _params.Item7);
            result.SelectedBackground = _selectionBackground;
            result.SelectedForeground = _selectionForeground;
            result.Spacing = _spacing;
            return result;
        }
    }
}
